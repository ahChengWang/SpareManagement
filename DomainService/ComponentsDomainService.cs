using SpareManagement.DomainService.Entity;
using SpareManagement.Enum;
using SpareManagement.Helper;
using SpareManagement.Repository;
using SpareManagement.Repository.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace SpareManagement.DomainService
{
    public class ComponentsDomainService
    {
        private readonly ComponentsRepository _componentsRepository;
        private readonly HistoryDomainService _historyDomainService;

        public ComponentsDomainService(ComponentsRepository componentsRepository,
            HistoryDomainService historyDomainService)
        {
            _componentsRepository = componentsRepository;
            _historyDomainService = historyDomainService;
        }


        public List<ComponentsEntity> Get(string partNo, string name, string purchaseId)
        {
            try
            {
                var t = _componentsRepository.SelectByConditions(null, partNo ?? "", name ?? "", purchaseId ?? "");

                var te = t.CopyAToB<ComponentsEntity>();

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
                List<ComponentsDao> _insComponents = new List<ComponentsDao>();

                var _updComponents = _componentsRepository.SelectByConditions(partNoList: warehouseGoodsList.Select(s => s.PartNo));

                var _allComponents = (from basic in basicList
                                      join input in warehouseGoodsList
                                      on basic.PartNo equals input.PartNo
                                      select new ComponentsDao
                                      {
                                          PartNo = basic.PartNo,
                                          Name = basic.Name,
                                          Spec = basic.Spec,
                                          PurchaseId = basic.PurchaseId,
                                          IsKeySpare = basic.IsKeySpare,
                                          IsCommercial = basic.IsCommercial,
                                          Equipment = basic.Equipment,
                                          Placement = basic.Placement,
                                          Inventory = input.Count,
                                          SafetyCount = basic.SafetyCount,
                                          Memo = basic.Memo
                                      }).ToList();

                _updComponents = (from all in _allComponents
                                  join upd in _updComponents
                                   on all.PartNo equals upd.PartNo
                                  select new ComponentsDao
                                  {
                                      PartNo = all.PartNo,
                                      Inventory = all.Inventory + upd.Inventory
                                  }).ToList();


                _insComponents = _allComponents.Where(w => !_updComponents.Select(s => s.PartNo).Contains(w.PartNo)).ToList();

                using (var scope = new TransactionScope())
                {
                    var _insResult = true;
                    var _updResult = true;

                    if (_insComponents.Any())
                        _insResult =  _componentsRepository.Insert(_insComponents) == _insComponents.Count;                    

                    if (_updComponents.Any())
                        _updResult = _componentsRepository.Update(_updComponents) == _updComponents.Count;

                    var _insHistory = new List<HistoryEntity>();

                    _allComponents.ForEach(fe =>
                    {
                        _insHistory.Add(new HistoryEntity
                        {
                            CategoryId = 2,
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
                        return "Insert & Update not success.";
                }

                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string ProcessRelease(
            IEnumerable<ReleaseGoodsEntity> releaseGoodsList,
            string createUser,
            DateTime createDate,
            string memo)
        {
            try
            {
                List<ComponentsDao> _updComponents = new List<ComponentsDao>();
                string _errMsg = "";

                var _releaseComponents = _componentsRepository.SelectByConditions(partNoList: releaseGoodsList.Select(s => s.PartNo));

                var _notExist = releaseGoodsList.Where(w => !_releaseComponents.Select(s => s.PartNo).Contains(w.PartNo));
                var _notEnough = _releaseComponents.Where(w => releaseGoodsList.FirstOrDefault(fd => fd.PartNo == w.PartNo).Count > w.Inventory);

                if (!_releaseComponents.Any())
                    _errMsg += $"查無設備零件 \n";

                if (_notExist.Any())
                    _notExist.ToList().ForEach(fe => _errMsg += $"{fe.PartNo} 無此設備零件 \n");

                if (_notEnough.Any())
                    _notEnough.ToList().ForEach(fe => _errMsg += $"{fe.PartNo} 庫存不足 \n");

                if (!string.IsNullOrEmpty(_errMsg))
                    return _errMsg;

                _updComponents = (from all in _releaseComponents
                                  join release in releaseGoodsList
                                   on all.PartNo equals release.PartNo
                                  select new ComponentsDao
                                  {
                                      PartNo = all.PartNo,
                                      Inventory = all.Inventory - release.Count
                                  }).ToList();

                using (var scope = new TransactionScope())
                {
                    var _updResult = true;

                    if (_updComponents.Any())
                    {
                        _updResult = _componentsRepository.Update(_updComponents) == _updComponents.Count;
                    }

                    var _insHistory = new List<HistoryEntity>();

                    releaseGoodsList.ToList().ForEach(fe =>
                    {
                        _insHistory.Add(new HistoryEntity
                        {
                            CategoryId = 2,
                            PartNo = fe.PartNo,
                            Status = StatusEnum.UnStock,
                            Quantity = fe.Count,
                            EmpName = createUser,
                            UpdateTime = createDate,
                            Memo = memo
                        });
                    });

                    _historyDomainService.Insert(_insHistory);

                    if (_updResult)
                        scope.Complete();
                    else
                        return "Insert & Update not success.";
                }

                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
