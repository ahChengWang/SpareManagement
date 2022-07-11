using SpareManagement.DomainService.Entity;
using SpareManagement.Enum;
using SpareManagement.Helper;
using SpareManagement.Repository;
using SpareManagement.Repository.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace SpareManagement.DomainService
{
    public class ExpendablesDomainService : IExpendablesDomainService
    {
        private readonly IExpendablesRepository _expendablesRepository;
        private readonly IHistoryDomainService _historyDomainService;

        public ExpendablesDomainService(IExpendablesRepository expendablesRepository,
            IHistoryDomainService historyDomainService)
        {
            _expendablesRepository = expendablesRepository;
            _historyDomainService = historyDomainService;
        }

        public List<ExpendablesEntity> Get(string partNo, string name, string purchaseId)
        {
            try
            {
                var t = _expendablesRepository.SelectByConditions(null, partNo ?? "", name ?? "", purchaseId ?? "");

                var te = t.CopyAToB<ExpendablesEntity>();

                return te;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ProcessWarehouse(
            IEnumerable<BasicInformationDao> basicList,
            List<WarehouseGoodsEntity> warehouseGoodsList,
            string createUser,
            DateTime createDate,
            string memo)
        {
            try
            {

                List<ExpendablesDao> _insExpendables = new List<ExpendablesDao>();

                var _updExpendables = _expendablesRepository.SelectByConditions(partNoList: warehouseGoodsList.Select(s => s.PartNo));

                var _allExpendables = (from basic in basicList
                                       join input in warehouseGoodsList
                                       on basic.PartNo equals input.PartNo
                                       select new ExpendablesDao
                                       {
                                           PartNo = basic.PartNo,
                                           Name = basic.Name,
                                           Spec = basic.Spec,
                                           PurchaseId = basic.PurchaseId,
                                           IsKeySpare = basic.IsKeySpare,
                                           IsCommercial = basic.IsCommercial,
                                           Placement = basic.Placement,
                                           Inventory = input.Count,
                                           SafetyCount = basic.SafetyCount,
                                           Memo = basic.Memo
                                       }).ToList();

                _updExpendables = (from all in _allExpendables
                                   join upd in _updExpendables
                                   on all.PartNo equals upd.PartNo
                                   select new ExpendablesDao
                                   {
                                       PartNo = all.PartNo,
                                       Inventory = all.Inventory + upd.Inventory
                                   }).ToList();


                _insExpendables = _allExpendables.Where(w => !_updExpendables.Select(s => s.PartNo).Contains(w.PartNo)).ToList();

                using (var scope = new TransactionScope())
                {
                    var _insResult = true;
                    var _updResult = true;

                    if (_insExpendables.Any())
                    {
                        _insResult = _expendablesRepository.Insert(_insExpendables) == _insExpendables.Count;
                    }
                    if (_updExpendables.Any())
                    {
                        _updResult = _expendablesRepository.Update(_updExpendables) == _updExpendables.Count;
                    }

                    var _insHistory = new List<HistoryEntity>();

                    _allExpendables.ForEach(fe =>
                    {
                        _insHistory.Add(new HistoryEntity
                        {
                            CategoryId = 1,
                            PartNo = fe.PartNo,
                            Status = StatusEnum.Stock,
                            Quantity = fe.Inventory,
                            EmpName = createUser,
                            UpdateTime = createDate,
                            Memo = memo
                        });
                    });

                    _historyDomainService.Insert(_insHistory);

                    if (_insResult && _updResult)
                        scope.Complete();
                    else
                        return "Insert & Update not success. \n";
                }

                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public (string, int) ProcessRelease(
            IEnumerable<ReleaseGoodsEntity> releaseGoodsList,
            string createUser,
            DateTime createDate,
            string memo)
        {
            try
            {
                List<ExpendablesDao> _updExpendables = new List<ExpendablesDao>();
                string _errMsg = "";

                var _releaseExpendables = _expendablesRepository.SelectByConditions(partNoList: releaseGoodsList.Select(s => s.PartNo));

                var _notExist = releaseGoodsList.Where(w => !_releaseExpendables.Select(s => s.PartNo).Contains(w.PartNo));
                var _notEnough = _releaseExpendables.Where(w => releaseGoodsList.FirstOrDefault(fd => fd.PartNo == w.PartNo).Count > w.Inventory);

                if (!_releaseExpendables.Any())
                    _errMsg += $"{string.Join("、", releaseGoodsList.Select(s => s.PartNo))} 查無耗材 \n";

                if (_notExist.Any())
                    _notExist.ToList().ForEach(fe => _errMsg += $"{fe.PartNo} 無此耗材 \n");

                if (_notEnough.Any())
                    _notEnough.ToList().ForEach(fe => _errMsg += $"{fe.PartNo} 庫存不足 \n");

                //if (!string.IsNullOrEmpty(_errMsg))
                //    return _errMsg;

                _updExpendables = (from all in _releaseExpendables.Except(_notEnough)
                                   join release in releaseGoodsList
                                    on all.PartNo equals release.PartNo
                                   select new ExpendablesDao
                                   {
                                       PartNo = all.PartNo,
                                       Inventory = all.Inventory - release.Count
                                   }).ToList();

                if (!_updExpendables.Any())
                    return (_errMsg, 0);

                using (var scope = new TransactionScope())
                {
                    var _updResult = true;

                    _updResult = _expendablesRepository.Update(_updExpendables) == _updExpendables.Count;

                    var _insHistory = new List<HistoryEntity>();

                    releaseGoodsList.ToList().ForEach(fe =>
                    {
                        _insHistory.Add(new HistoryEntity
                        {
                            CategoryId = 1,
                            PartNo = fe.PartNo,
                            Status = StatusEnum.UnStock,
                            Quantity = fe.Count,
                            EmpName = createUser,
                            UpdateTime = createDate,
                            Memo = memo,
                            Node = fe.Node
                        });
                    });

                    _historyDomainService.Insert(_insHistory);

                    if (_updResult)
                        scope.Complete();
                    else
                        return ("Insert & Update not success.", 0);
                }

                return ("", _updExpendables.Count);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
