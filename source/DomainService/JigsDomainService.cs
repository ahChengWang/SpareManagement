using Helper;
using SpareManagement.DomainService.Entity;
using SpareManagement.Enum;
using SpareManagement.Repository;
using SpareManagement.Repository.Dao;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Transactions;

namespace SpareManagement.DomainService
{
    public class JigsDomainService : IJigsDomainService
    {
        private readonly IJigsRepository _jigsRepository;
        private readonly IHistoryDomainService _historyDomainService;
        private readonly IBasicInformationRepository _basicInformationRepository;
        private readonly IBasicInformationDomainService _basicInformationDomainService;

        public JigsDomainService(IJigsRepository jigsRepository,
            IHistoryDomainService historyDomainService,
            IBasicInformationRepository basicInformationRepository,
            IBasicInformationDomainService basicInformationDomainService)
        {
            _jigsRepository = jigsRepository;
            _historyDomainService = historyDomainService;
            _basicInformationRepository = basicInformationRepository;
            _basicInformationDomainService = basicInformationDomainService;
        }


        public List<JigsEntity> Get(string partNo, string name, string purchaseId)
        {
            try
            {
                var _partNoListBySerialNo = GetDetail(null, serialNo: partNo, isForMainPage: true).GroupBy(gb => gb.PartNo).Select(s => s.Key).ToList();
                if (partNo != null)
                    _partNoListBySerialNo.Add(partNo);
                var _jigsList = GetDetail("", name, purchaseId, partNoList: _partNoListBySerialNo);

                var _jigsListGp = _jigsList.GroupBy(gb => new { gb.PartNo, gb.Name, gb.PurchaseId, gb.Spec, gb.IsKeySpare, gb.IsCommercial, gb.Equipment, gb.SafetyCount, gb.Memo })
                    .Select(se => new JigsEntity
                    {
                        PartNo = se.Key.PartNo,
                        Name = se.Key.Name,
                        PurchaseId = se.Key.PurchaseId,
                        Spec = se.Key.Spec,
                        IsKeySpare = se.Key.IsKeySpare,
                        IsCommercial = se.Key.IsCommercial,
                        Equipment = se.Key.Equipment,
                        Count = se.Count(),
                        Inventory = se.Where(w => w.Status == StatusEnum.Stock).Count(),
                        SafetyCount = se.Key.SafetyCount,
                        Memo = se.Key.Memo
                    }).ToList();

                return _jigsListGp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<JigsEntity> GetDetail(
            string partNo,
            string name = "",
            string purchaseId = "",
            string serialNo = "",
            IEnumerable<string> serialNoList = null,
            int statusId = 0,
            DateTime? beginInspectDate = null,
            DateTime? endInspectDate = null,
            bool isForMainPage = false,
            List<string> partNoList = null)
        {
            try
            {
                var _jigsDao = _jigsRepository.SelectByConditions(
                    partNoList,
                    partNo ?? "",
                    serialNo ?? "",
                    name ?? "",
                    purchaseId ?? "",
                    serialNoList,
                    statusId,
                    beginInspectDate,
                    endInspectDate,
                    isForMainPage);

                var _res = new List<JigsEntity>();

                DateTime _now = DateTime.Now.Date;

                _jigsDao.ForEach(fe =>
                {
                    _res.Add(new JigsEntity
                    {
                        PartNo = fe.PartNo,
                        Name = fe.Name,
                        SerialNo = fe.SerialNo,
                        Status = fe.Status,
                        PurchaseId = fe.PurchaseId,
                        Spec = fe.Spec,
                        Placement = fe.Placement,
                        IsKeySpare = fe.IsKeySpare,
                        IsCommercial = fe.IsCommercial,
                        Inventory = fe.Inventory,
                        Equipment = fe.Equipment,
                        IsNeedInspect = fe.IsNeedInspect,
                        InspectDate = fe.InspectDate,
                        NextInspectDate = fe.NextInspectDate,
                        IsOverdueInspect = _now > fe.NextInspectDate,
                        SafetyCount = fe.SafetyCount,
                        IsTemporary = fe.IsTemporary
                    });
                });

                return _res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JigsEntity GetEditData(string partNo, string serialNo)
        {
            try
            {
                var _editData = GetDetail(partNo, serialNo: serialNo).FirstOrDefault();

                if (_editData.Status != StatusEnum.Stock)
                {
                    throw new Exception($"狀態不為[{StatusEnum.Stock.GetDescription()}] 已無法編輯");
                }

                return _editData;
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

                List<JigsDao> _insJigs = new List<JigsDao>();
                List<BasicInformationDao> _updBasicLastSerial = new List<BasicInformationDao>();

                //var _updJigs = _jigsRepository.SelectByConditions(partNoList: warehouseGoodsList.Select(s => s.PartNo));

                warehouseGoodsList = warehouseGoodsList.Where(w => basicList.Select(s => s.PartNo).Contains(w.PartNo)).ToList();

                foreach (var good in warehouseGoodsList)
                {
                    var tmpBasic = basicList.FirstOrDefault(f => f.PartNo == good.PartNo);

                    for (int i = 0; i < good.Count; i++)
                    {
                        _insJigs.Add(new JigsDao
                        {
                            PartNo = tmpBasic.PartNo,
                            Name = tmpBasic.Name,
                            SerialNo = $"{tmpBasic.PartNo}_{(tmpBasic.LastSerialNo + (i + 1)).ToString("0000")}",
                            Status = StatusEnum.Stock,
                            Spec = tmpBasic.Spec,
                            PurchaseId = tmpBasic.PurchaseId,
                            IsKeySpare = tmpBasic.IsKeySpare,
                            IsCommercial = tmpBasic.IsCommercial,
                            Equipment = tmpBasic.Equipment,
                            Placement = tmpBasic.Placement,
                            Inventory = 1,
                            SafetyCount = tmpBasic.SafetyCount,
                            IsOverdueInspect = false,
                            IsTemporary = false,
                            IsNeedInspect = tmpBasic.IsNeedInspect,
                            InspectDate = null,
                            NextInspectDate = null,
                            Memo = tmpBasic.Memo
                        });
                    }

                    _updBasicLastSerial.Add(new BasicInformationDao
                    {
                        CategoryId = tmpBasic.CategoryId,
                        PartNo = tmpBasic.PartNo,
                        LastSerialNo = tmpBasic.LastSerialNo + good.Count
                    });
                }

                var _insHistory = new List<HistoryEntity>();

                _insJigs.ForEach(fe =>
                {
                    _insHistory.Add(new HistoryEntity
                    {
                        CategoryId = 3,
                        PartNo = fe.SerialNo,
                        Status = StatusEnum.Stock,
                        Quantity = fe.Inventory,
                        EmpName = createUser,
                        UpdateTime = createDate,
                        Memo = memo
                    });
                });

                using (var scope = new TransactionScope())
                {
                    var _insResult = true;
                    var _updResult = true;

                    _insResult = _jigsRepository.Insert(_insJigs) == _insJigs.Count;

                    _updResult = _basicInformationRepository.UpdateLastSerialNo(_updBasicLastSerial) == _updBasicLastSerial.Count;

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

        public string UpdateInspectAndTemporary(string partNo, string serialNo, string inspectDate, bool temporary, string placement)
        {
            try
            {
                DateTime? newInspectDate = null;
                DateTime convDate;
                DateTime _nowDate = DateTime.Now;

                if (inspectDate != null && DateTime.TryParseExact(inspectDate, "yyyy-MM-dd", null, DateTimeStyles.None, out convDate))
                    newInspectDate = convDate;

                var _basicInfoeData = _basicInformationDomainService.GetInspectInfo(new BasicInfoEntity { PartNo = partNo });
                var _oldJigsData = _jigsRepository.SelectByConditions(serialNo: serialNo).FirstOrDefault();

                if (_oldJigsData.Status != StatusEnum.Stock)
                    throw new Exception($"目前狀態[{_oldJigsData.Status.GetDescription()}]暫時無法更新");

                var _updJigsData = new JigsDao
                {
                    SerialNo = serialNo,
                    InspectDate = _oldJigsData.InspectDate != null ? _oldJigsData.InspectDate : newInspectDate,
                    Placement = placement,
                    IsTemporary = temporary,
                    IsOverdueInspect = newInspectDate != null && _nowDate > newInspectDate,
                    NextInspectDate = newInspectDate != null ? ((DateTime)newInspectDate).AddDays(_basicInfoeData.InspectCycle) : (DateTime?)null
                };

                if (_oldJigsData.InspectDate != null)
                {
                    _updJigsData.InspectDate = _oldJigsData.InspectDate;
                    _updJigsData.NextInspectDate = _oldJigsData.NextInspectDate;
                }
                else if (newInspectDate != null)
                {
                    _updJigsData.InspectDate = newInspectDate;
                    _updJigsData.NextInspectDate = ((DateTime)newInspectDate).AddDays(_basicInfoeData.InspectCycle);
                }

                var _insHistory = new List<HistoryEntity>();

                _insHistory.Add(new HistoryEntity
                {
                    CategoryId = 3,
                    PartNo = serialNo,
                    Status = _oldJigsData.Status,
                    Quantity = _oldJigsData.Inventory,
                    EmpName = "",
                    UpdateTime = _nowDate,
                    Memo = "欄位更新"
                });

                using (var scope = new TransactionScope())
                {
                    var _insResult = true;

                    _insResult = _jigsRepository.Update(new List<JigsDao> { _updJigsData }) == 1;

                    _historyDomainService.Insert(_insHistory);

                    if (_insResult)
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

        public string Update(string serialNo, StatusEnum oldStatusId, StatusEnum newStatusId, string updateUser, DateTime updateDTE, int? inspectCycle = null, string errSummary = "")
        {
            try
            {
                var _origJigsData = _jigsRepository.SelectByConditions(serialNo: serialNo).FirstOrDefault();

                if (_origJigsData.Status != oldStatusId)
                    return $"狀態不為 [{ oldStatusId.GetDescription()}] 無法{errSummary}";
                if (_origJigsData.Status == newStatusId)
                    return $"狀態已為 [{ newStatusId.GetDescription()}]";
                if (_origJigsData.IsTemporary)
                    return $"{serialNo} 暫停使用無法{errSummary}";

                string _memo = "";

                switch (oldStatusId)
                {
                    case StatusEnum.UnStock:
                        _memo = "領用歸還";
                        break;
                    case StatusEnum.Inspecting:
                        _memo = "檢驗歸還";
                        break;
                    case StatusEnum.Fixing:
                        _memo = "維修歸還";
                        break;
                    default:
                        _memo = newStatusId.GetDescription();
                        break;
                }

                var _updJigsData = new JigsDao
                {
                    SerialNo = serialNo,
                    Status = newStatusId,
                    Inventory = newStatusId == StatusEnum.Stock ? _origJigsData.Inventory + 1 : _origJigsData.Inventory - 1
                };

                if (inspectCycle != null)
                {
                    _updJigsData.InspectDate = updateDTE;
                    _updJigsData.NextInspectDate = updateDTE.AddDays((int)inspectCycle);
                }

                var _insHistory = new HistoryEntity
                {
                    CategoryId = 3,
                    PartNo = _updJigsData.SerialNo,
                    Status = newStatusId,
                    Quantity = 1,
                    EmpName = updateUser,
                    UpdateTime = updateDTE,
                    Memo = _memo
                };

                using (var scope = new TransactionScope())
                {
                    var _updResult = true;

                    _updResult = _jigsRepository.UpdateStatus(_updJigsData) == 1;

                    _historyDomainService.Insert(new List<HistoryEntity> { _insHistory });

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

        public (string, int) ProcessRelease(
            IEnumerable<ReleaseGoodsEntity> releaseJigsList,
            string createUser,
            DateTime createDate,
            string memo)
        {
            try
            {
                List<JigsDao> _updJigs = new List<JigsDao>();
                string _errMsg = "";

                var _stockJigs = _jigsRepository.SelectByConditions(serialNoList: releaseJigsList.Select(s => s.PartNo));

                var _notExist = releaseJigsList.Where(w => !_stockJigs.Select(s => s.SerialNo).Contains(w.PartNo));
                var _jigsConuntError = _stockJigs.Where(w => releaseJigsList.Where(w => w.Count > 1).Select(s => s.PartNo).Contains(w.SerialNo));
                var _jigsStatusError = _stockJigs.Where(w => w.Status != StatusEnum.Stock || w.IsTemporary);

                if (!_stockJigs.Any())
                    _errMsg += $"{string.Join("、", releaseJigsList.Select(s => s.PartNo))} 查無治具 \n";

                if (_jigsConuntError.Any())
                    _jigsConuntError.ToList().ForEach(fe => _errMsg += $"{fe.SerialNo} 治具領用數量不能 > 1 \n");

                if (_notExist.Any())
                    _notExist.ToList().ForEach(fe => _errMsg += $"{fe.PartNo} 確認治具序號及狀態 \n");

                if (_jigsStatusError.Any())
                    _jigsStatusError.ToList().ForEach(fe => _errMsg += $"{fe.SerialNo} 治具 [{fe.Status.GetDescription()}]或暫停使用 無法領用 \n");

                //if (!string.IsNullOrEmpty(_errMsg))
                //    return _errMsg;

                _updJigs = (from all in _stockJigs.Except(_jigsConuntError).Except(_jigsStatusError)
                            join release in releaseJigsList
                             on all.SerialNo equals release.PartNo
                            select new JigsDao
                            {
                                SerialNo = all.SerialNo,
                                Status = StatusEnum.UnStock,
                                Inventory = all.Inventory - release.Count
                            }).ToList();

                if (!_updJigs.Any())
                    return (_errMsg, 0);

                var _insHistory = new List<HistoryEntity>();

                releaseJigsList.ToList().ForEach(fe =>
                {
                    _insHistory.Add(new HistoryEntity
                    {
                        CategoryId = 3,
                        PartNo = fe.PartNo,
                        Status = StatusEnum.UnStock,
                        Quantity = fe.Count,
                        EmpName = createUser,
                        UpdateTime = createDate,
                        Memo = memo,
                        Node = fe.Node
                    });
                });

                using (var scope = new TransactionScope())
                {
                    var _updResult = true;

                    _updResult = _jigsRepository.UpdateRelease(_updJigs) == _updJigs.Count;

                    _historyDomainService.Insert(_insHistory);

                    if (_updResult)
                        scope.Complete();
                    else
                        return ("Insert & Update not success.", 0);
                }

                return ("", _updJigs.Count);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
