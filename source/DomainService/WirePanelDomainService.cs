using Helper;
using SpareManagement.DomainService.Entity;
using SpareManagement.Enum;
using SpareManagement.Repository;
using SpareManagement.Repository.Dao;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Transactions;

namespace SpareManagement.DomainService
{
    public class WirePanelDomainService
    {
        private readonly WirePanelRepository _wirePanelRepository;
        private readonly HistoryDomainService _historyDomainService;
        private readonly BasicInformationRepository _basicInformationRepository;
        private readonly BasicInformationDomainService _basicInformationDomainService;

        public WirePanelDomainService(WirePanelRepository wirePanelRepository,
            HistoryDomainService historyDomainService,
            BasicInformationRepository basicInformationRepository,
            BasicInformationDomainService basicInformationDomainService)
        {
            _wirePanelRepository = wirePanelRepository;
            _historyDomainService = historyDomainService;
            _basicInformationRepository = basicInformationRepository;
            _basicInformationDomainService = basicInformationDomainService;
        }


        public List<WirePanelEntity> Get(string partNo, string name, string purchaseId)
        {
            try
            {
                var _partNoListBySerialNo = GetDetail(null, serialNo: partNo, isForMainPage: true).GroupBy(gb => gb.PartNo).Select(s => s.Key).ToList();
                if (partNo != null)
                    _partNoListBySerialNo.Add(partNo);
                var _wirePanelList = GetDetail("", name, purchaseId, partNoList: _partNoListBySerialNo);

                var _wirePanelGp = _wirePanelList.GroupBy(gb => new { gb.PartNo, gb.Name, gb.PurchaseId, gb.Spec, gb.IsKeySpare, gb.IsCommercial, gb.Equipment, gb.SafetyCount, gb.Memo })
                    .Select(se => new WirePanelEntity
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

                return _wirePanelGp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<WirePanelEntity> GetDetail(
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
                var _wirePanelDao = _wirePanelRepository.SelectByConditions(
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

                var _res = new List<WirePanelEntity>();

                DateTime _now = DateTime.Now.Date;

                _wirePanelDao.ForEach(fe =>
                {
                    _res.Add(new WirePanelEntity
                    {
                        PartNo = fe.PartNo,
                        Name = fe.Name,
                        SerialNo = fe.SerialNo,
                        Status = fe.Status,
                        PurchaseId = fe.PurchaseId,
                        Spec = fe.Spec,
                        Placement = fe.Placement,
                        Equipment = fe.Equipment,
                        Inventory = fe.Inventory,
                        IsKeySpare = fe.IsKeySpare,
                        IsCommercial = fe.IsCommercial,
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

        public WirePanelEntity GetEditData(string partNo, string serialNo)
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
                List<WirePanelDao> _insWirePanel = new List<WirePanelDao>();
                List<BasicInformationDao> _updBasicLastSerial = new List<BasicInformationDao>();

                warehouseGoodsList = warehouseGoodsList.Where(w => basicList.Select(s => s.PartNo).Contains(w.PartNo)).ToList();

                foreach (var good in warehouseGoodsList)
                {
                    var tmpBasic = basicList.FirstOrDefault(f => f.PartNo == good.PartNo);

                    for (int i = 0; i < good.Count; i++)
                    {
                        _insWirePanel.Add(new WirePanelDao
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

                _insWirePanel.ForEach(fe =>
                {
                    _insHistory.Add(new HistoryEntity
                    {
                        CategoryId = 4,
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

                    _insResult = _wirePanelRepository.Insert(_insWirePanel) == _insWirePanel.Count;

                    _updResult = _basicInformationRepository.UpdateLastSerialNo(_updBasicLastSerial) == _updBasicLastSerial.Count;

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
                var _oldwirePanelData = _wirePanelRepository.SelectByConditions(serialNo: serialNo).FirstOrDefault();

                if (_oldwirePanelData.Status != StatusEnum.Stock)
                    throw new Exception($"目前狀態[{_oldwirePanelData.Status.GetDescription()}]暫時無法更新");

                var _updWirePanelData = new WirePanelDao
                {
                    SerialNo = serialNo,
                    InspectDate = _oldwirePanelData.InspectDate != null ? _oldwirePanelData.InspectDate : newInspectDate,
                    Placement = placement,
                    IsTemporary = temporary,
                    IsOverdueInspect = newInspectDate != null && _nowDate > newInspectDate,
                    NextInspectDate = newInspectDate != null ? ((DateTime)newInspectDate).AddDays(_basicInfoeData.InspectCycle) : (DateTime?)null
                };

                if (_oldwirePanelData.InspectDate != null)
                {
                    _updWirePanelData.InspectDate = _oldwirePanelData.InspectDate;
                    _updWirePanelData.NextInspectDate = _oldwirePanelData.NextInspectDate;
                }
                else if (newInspectDate != null)
                {
                    _updWirePanelData.InspectDate = newInspectDate;
                    _updWirePanelData.NextInspectDate = ((DateTime)newInspectDate).AddDays(_basicInfoeData.InspectCycle);
                }

                var _insHistory = new List<HistoryEntity>();

                _insHistory.Add(new HistoryEntity
                {
                    CategoryId = 4,
                    PartNo = serialNo,
                    Status = _oldwirePanelData.Status,
                    Quantity = _oldwirePanelData.Inventory,
                    EmpName = "",
                    UpdateTime = _nowDate,
                    Memo = "欄位更新"
                });

                using (var scope = new TransactionScope())
                {
                    var _insResult = true;

                    _insResult = _wirePanelRepository.Update(new List<WirePanelDao> { _updWirePanelData }) == 1;

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
                var _origWirePanelData = _wirePanelRepository.SelectByConditions(serialNo: serialNo).FirstOrDefault();

                if (_origWirePanelData.Status != oldStatusId)
                    return $"狀態不為 [{ oldStatusId.GetDescription()}] 無法{errSummary}";
                if (_origWirePanelData.Status == newStatusId)
                    return $"狀態已為 [{ newStatusId.GetDescription()}]";
                if (_origWirePanelData.IsTemporary)
                    return $"暫停使用無法{errSummary}";

                var _updWirePanelData = new WirePanelDao
                {
                    SerialNo = serialNo,
                    Status = newStatusId,
                    Inventory = newStatusId == StatusEnum.Stock ? _origWirePanelData.Inventory + 1 : _origWirePanelData.Inventory - 1
                };

                if (inspectCycle != null)
                {
                    _updWirePanelData.InspectDate = updateDTE;
                    _updWirePanelData.NextInspectDate = updateDTE.AddDays((int)inspectCycle);
                }

                var _insHistory = new HistoryEntity
                {
                    CategoryId = 4,
                    PartNo = _updWirePanelData.SerialNo,
                    Status = newStatusId,
                    Quantity = 1,
                    EmpName = updateUser,
                    UpdateTime = updateDTE,
                    Memo = ""
                };

                using (var scope = new TransactionScope())
                {
                    var _updResult = true;

                    _updResult = _wirePanelRepository.UpdateStatus(_updWirePanelData) == 1;


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
            IEnumerable<ReleaseGoodsEntity> releaseWirePanelList,
            string createUser,
            DateTime createDate,
            string memo)
        {
            try
            {
                List<WirePanelDao> _updWirePanel = new List<WirePanelDao>();
                string _errMsg = "";

                var _stockWirePanel = _wirePanelRepository.SelectByConditions(serialNoList: releaseWirePanelList.Select(s => s.PartNo));

                var _notExist = releaseWirePanelList.Where(w => !_stockWirePanel.Select(s => s.SerialNo).Contains(w.PartNo));
                var _wirepanelConuntError = _stockWirePanel.Where(w => releaseWirePanelList.Where(w => w.Count > 1).Select(s => s.PartNo).Contains(w.SerialNo));
                var _wirepanelStatusError = _stockWirePanel.Where(w => w.Status != StatusEnum.Stock || w.IsTemporary);

                if (!_stockWirePanel.Any())
                    _errMsg += $"{string.Join("、", releaseWirePanelList.Select(s => s.PartNo))} 查無線板材 \n";

                if (_wirepanelConuntError.Any())
                    _wirepanelConuntError.ToList().ForEach(fe => _errMsg += $"{fe.SerialNo} 線板材領用數量不能 > 1 \n");

                if (_notExist.Any())
                    _notExist.ToList().ForEach(fe => _errMsg += $"{fe.PartNo} 確認線板材序號及狀態 \n");

                if (_wirepanelStatusError.Any())
                    _wirepanelStatusError.ToList().ForEach(fe => _errMsg += $"{fe.SerialNo} 線板材 [{fe.Status.GetDescription()}] 或暫停使用無法領用 \n");

                //if (!string.IsNullOrEmpty(_errMsg))
                //    return (_errMsg, 0);

                _updWirePanel = (from all in _stockWirePanel.Except(_wirepanelConuntError).Except(_wirepanelStatusError)
                                 join release in releaseWirePanelList
                                  on all.SerialNo equals release.PartNo
                                 select new WirePanelDao
                                 {
                                     SerialNo = all.SerialNo,
                                     Status = StatusEnum.UnStock,
                                     Inventory = all.Inventory - release.Count
                                 }).ToList();

                if (!_updWirePanel.Any())
                    return (_errMsg, 0);

                var _insHistory = new List<HistoryEntity>();

                releaseWirePanelList.ToList().ForEach(fe =>
                {
                    _insHistory.Add(new HistoryEntity
                    {
                        CategoryId = 4,
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

                    _updResult = _wirePanelRepository.UpdateRelease(_updWirePanel) == _updWirePanel.Count;

                    _historyDomainService.Insert(_insHistory);

                    if (_updResult)
                        scope.Complete();
                    else
                        return ("Insert & Update not success.", 0);
                }

                return ("", _updWirePanel.Count);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
