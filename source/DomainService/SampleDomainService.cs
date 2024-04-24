using Helper;
using SpareManagement.DomainService.Entity;
using SpareManagement.Enum;
using SpareManagement.Helper;
using SpareManagement.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Transactions;

namespace SpareManagement.DomainService
{
    public class SampleDomainService : ISampleDomainService
    {
        private readonly ISampleRepository _sampleRepository;
        private readonly IHistoryDomainService _historyDomainService;
        private readonly IBasicInformationRepository _basicInformationRepository;

        public SampleDomainService(ISampleRepository sampleRepository,
            IHistoryDomainService historyDomainService,
            IBasicInformationRepository basicInformationRepository)
        {
            _sampleRepository = sampleRepository;
            _historyDomainService = historyDomainService;
            _basicInformationRepository = basicInformationRepository;
        }


        public List<SampleEntity> Get(string partNo, string name, string purchaseId)
        {
            try
            {
                var _partNoListBySerialNo = GetDetail(null, serialNo: partNo, isForMainPage: true).GroupBy(gb => gb.PartNo).Select(s => s.Key).ToList();
                if (partNo != null)
                    _partNoListBySerialNo.Add(partNo);
                var _sampleList = GetDetail("", name, purchaseId, partNoList: _partNoListBySerialNo);

                var _sampleListGp = _sampleList.GroupBy(gb => new { gb.PartNo, gb.Name, gb.PurchaseId, gb.Spec, gb.IsKeySpare, gb.IsCommercial, gb.Equipment, gb.SafetyCount, gb.Memo })
                    .Select(se => new SampleEntity
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

                return _sampleListGp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SampleEntity> GetDetail(
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
                var _sampleDao = _sampleRepository.SelectByConditions(
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

                var _res = new List<SampleEntity>();

                DateTime _now = DateTime.Now.Date;

                _sampleDao.ForEach(fe =>
                {
                    _res.Add(new SampleEntity
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
                        IsTemporary = fe.IsTemporary,
                        Memo = fe.Memo
                    });
                });

                return _res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SampleEntity GetEditData(string partNo, string serialNo)
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
            DateTime? createDate,
            string memo,
            DateTime nowTime,
            UserEntity userEntity)
        {
            try
            {
                string _resp = "";
                DateTime _nowTime = DateTime.Now;
                List<SampleDao> _insSamples = new List<SampleDao>();
                List<BasicInformationDao> _updBasicLastSerial = new List<BasicInformationDao>();

                warehouseGoodsList = warehouseGoodsList.Where(w => basicList.Select(s => s.PartNo).Contains(w.PartNo)).ToList();

                foreach (var good in warehouseGoodsList)
                {
                    var tmpBasic = basicList.FirstOrDefault(f => f.PartNo == good.PartNo);

                    for (int i = 0; i < good.Count; i++)
                    {
                        _insSamples.Add(new SampleDao
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
                            NextInspectDate = tmpBasic.IsNeedInspect ? (DateTime?)_nowTime.AddDays(tmpBasic.InspectCycle) : null,
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

                _insSamples.ForEach(fe =>
                {
                    _insHistory.Add(new HistoryEntity
                    {
                        CategoryId = 5,
                        PartNo = fe.SerialNo,
                        Status = StatusEnum.Stock,
                        Quantity = fe.Inventory,
                        EmpName = string.IsNullOrEmpty(createUser) ? $"{userEntity.Account} {userEntity.Name}" : createUser,
                        UpdateTime = createDate ?? nowTime,
                        Memo = memo
                    });
                });

                using (var scope = new TransactionScope())
                {
                    var _insResult = true;
                    var _updResult = true;

                    _insResult = _sampleRepository.Insert(_insSamples) == _insSamples.Count;

                    _updResult = _basicInformationRepository.UpdateLastSerialNo(_updBasicLastSerial) == _updBasicLastSerial.Count;

                    _historyDomainService.Insert(_insHistory);

                    if (_insResult && _updResult)
                        scope.Complete();
                    else
                        _resp = "Insert & Update not success. \n";
                }

                return _resp;
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

                var _basicInfoeData = _basicInformationRepository.SelectByConditions(partNoList: new List<string> { partNo }).CopyAToB<BasicInfoEntity>().FirstOrDefault();
                var _oldSampleData = _sampleRepository.SelectByConditions(serialNo: serialNo).FirstOrDefault();

                if (_oldSampleData.Status != StatusEnum.Stock)
                    throw new Exception($"目前狀態[{_oldSampleData.Status.GetDescription()}]暫時無法更新");

                var _updSampleData = new SampleDao
                {
                    SerialNo = serialNo,
                    InspectDate = _oldSampleData.InspectDate != null ? _oldSampleData.InspectDate : newInspectDate,
                    Placement = placement,
                    IsTemporary = temporary,
                    IsOverdueInspect = newInspectDate != null && _nowDate > newInspectDate,
                    NextInspectDate = newInspectDate != null ? ((DateTime)newInspectDate).AddDays(_basicInfoeData.InspectCycle) : (DateTime?)null
                };

                if (_oldSampleData.InspectDate != null)
                {
                    _updSampleData.InspectDate = _oldSampleData.InspectDate;
                    _updSampleData.NextInspectDate = _oldSampleData.NextInspectDate;
                }
                else if (newInspectDate != null)
                {
                    _updSampleData.InspectDate = newInspectDate;
                    _updSampleData.NextInspectDate = ((DateTime)newInspectDate).AddDays(_basicInfoeData.InspectCycle);
                }

                var _insHistory = new List<HistoryEntity>();

                _insHistory.Add(new HistoryEntity
                {
                    CategoryId = 5,
                    PartNo = serialNo,
                    Status = _oldSampleData.Status,
                    Quantity = _oldSampleData.Inventory,
                    EmpName = "",
                    UpdateTime = _nowDate,
                    Memo = "欄位更新"
                });

                using (var scope = new TransactionScope())
                {
                    var _insResult = true;

                    _insResult = _sampleRepository.Update(new List<SampleDao> { _updSampleData }) == 1;

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

        public string Update(string serialNo, StatusEnum oldStatusId, StatusEnum newStatusId, string updateUser, DateTime? updateDTE, UserEntity userEntity, DateTime nowTime, int? inspectCycle = null, string errSummary = "")
        {
            try
            {
                var _origSampleData = _sampleRepository.SelectByConditions(serialNo: serialNo).FirstOrDefault();

                if (_origSampleData.Status != oldStatusId)
                    return $"狀態不為 [{ oldStatusId.GetDescription()}] 無法{errSummary}";
                if (_origSampleData.Status == newStatusId)
                    return $"狀態已為 [{ newStatusId.GetDescription()}]";
                if (_origSampleData.IsTemporary)
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

                var _updSampleData = new SampleDao
                {
                    SerialNo = serialNo,
                    Status = newStatusId,
                    Inventory = newStatusId == StatusEnum.Stock ? _origSampleData.Inventory + 1 : _origSampleData.Inventory - 1
                };

                if (inspectCycle != null)
                {
                    _updSampleData.InspectDate = updateDTE ?? nowTime;
                    _updSampleData.NextInspectDate = updateDTE?.AddDays((int)inspectCycle) ?? nowTime;
                }

                var _insHistory = new HistoryEntity
                {
                    CategoryId = 5,
                    PartNo = _updSampleData.SerialNo,
                    Status = newStatusId,
                    Quantity = 1,
                    EmpName = updateUser ?? $"{userEntity.Account} {userEntity.Name}",
                    UpdateTime = updateDTE ?? nowTime,
                    Memo = _memo
                };

                using (var scope = new TransactionScope())
                {
                    var _updResult = true;

                    _updResult = _sampleRepository.UpdateStatus(_updSampleData) == 1;

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
            IEnumerable<ReleaseGoodsEntity> releaseSampleList,
            string createUser,
            DateTime? createDate,
            string memo,
            DateTime nowTime,
            UserEntity userEntity)
        {
            try
            {
                List<SampleDao> _updSample = new List<SampleDao>();
                string _errMsg = "";

                var _stockSample = _sampleRepository.SelectByConditions(serialNoList: releaseSampleList.Select(s => s.PartNo));

                var _notExist = releaseSampleList.Where(w => !_stockSample.Select(s => s.SerialNo).Contains(w.PartNo));
                var _sampleConuntError = _stockSample.Where(w => releaseSampleList.Where(w => w.Count > 1).Select(s => s.PartNo).Contains(w.SerialNo));
                var _sampleStatusError = _stockSample.Where(w => w.Status != StatusEnum.Stock || w.IsTemporary);

                if (!_stockSample.Any())
                    _errMsg += $"{string.Join("、", releaseSampleList.Select(s => s.PartNo))} 查無sample \n";

                if (_sampleConuntError.Any())
                    _sampleConuntError.ToList().ForEach(fe => _errMsg += $"{fe.SerialNo} sample領用數量不能 > 1 \n");

                if (_notExist.Any())
                    _notExist.ToList().ForEach(fe => _errMsg += $"{fe.PartNo} 確認sample序號及狀態 \n");

                if (_sampleStatusError.Any())
                    _sampleStatusError.ToList().ForEach(fe => _errMsg += $"{fe.SerialNo} sample [{fe.Status.GetDescription()}]或暫停使用 無法領用 \n");

                //if (!string.IsNullOrEmpty(_errMsg))
                //    return _errMsg;

                _updSample = (from all in _stockSample.Except(_sampleConuntError).Except(_sampleStatusError)
                            join release in releaseSampleList
                             on all.SerialNo equals release.PartNo
                            select new SampleDao
                            {
                                SerialNo = all.SerialNo,
                                Status = StatusEnum.UnStock,
                                Inventory = all.Inventory - release.Count
                            }).ToList();

                if (!_updSample.Any())
                    return (_errMsg, 0);

                var _insHistory = new List<HistoryEntity>();

                releaseSampleList.ToList().ForEach(fe =>
                {
                    _insHistory.Add(new HistoryEntity
                    {
                        CategoryId = 5,
                        PartNo = fe.PartNo,
                        Status = StatusEnum.UnStock,
                        Quantity = fe.Count,
                        EmpName = string.IsNullOrEmpty(createUser) ? $"{userEntity.Account} {userEntity.Name}" : createUser,
                        UpdateTime = createDate ?? nowTime,
                        Memo = memo,
                        Node = fe.Node
                    });
                });

                using (var scope = new TransactionScope())
                {
                    var _updResult = true;

                    _updResult = _sampleRepository.UpdateRelease(_updSample) == _updSample.Count;

                    _historyDomainService.Insert(_insHistory);

                    if (_updResult)
                        scope.Complete();
                    else
                        return ("Insert & Update not success.", 0);
                }

                return (_errMsg, _updSample.Count);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdatePlacement(BasicInformationDao updDao)
        {
            try
            {
                _sampleRepository.UpdatePlacement(updDao.PartNo, updDao.Placement, updDao.SafetyCount, updDao.PurchaseId);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
