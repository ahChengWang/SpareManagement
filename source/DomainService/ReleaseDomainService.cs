using SpareManagement.DomainService.Entity;
using SpareManagement.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpareManagement.DomainService
{
    public class ReleaseDomainService : IReleaseDomainService
    {
        private readonly IBasicInformationRepository _basicInformationRepository;
        private readonly IExpendablesDomainService _expendablesDomainService;
        private readonly IComponentsDomainService _componentsDomainService;
        private readonly IJigsDomainService _jigsDomainService;
        private readonly IWirePanelDomainService _wirePanelDomainService;
        private readonly ISampleDomainService _sampleDomainService;

        public ReleaseDomainService(IBasicInformationRepository basicInformationRepository,
            IExpendablesDomainService expendablesDomainService,
            IComponentsDomainService componentsDomainService,
            IJigsDomainService jigsDomainService,
            IWirePanelDomainService wirePanelDomainService,
            ISampleDomainService sampleDomainService)
        {
            _basicInformationRepository = basicInformationRepository;
            _expendablesDomainService = expendablesDomainService;
            _componentsDomainService = componentsDomainService;
            _jigsDomainService = jigsDomainService;
            _wirePanelDomainService = wirePanelDomainService;
            _sampleDomainService = sampleDomainService;
        }

        public string Release(ReleaseEntity entity, UserEntity userEntity)
        {
            try
            {
                DateTime _nowTime = DateTime.Now;
                var _result = Validate(entity);

                if (_result.Item1 != "")
                    return _result.Item1;

                var basicDataList = _basicInformationRepository.SelectByConditions(partNoList: _result.Item2.Select(s => s.PartNo).ToList());
                var _processResult = "";
                var _processCnt = 0;
                (string, int) _tmpResult;

                //var _nonExistPartNo = string.Join(',', _result.Item2.Select(s => s.PartNo).Where(w => !basicDataList.Select(s => s.PartNo).Contains(w)));

                //if (basicDataList.Count() == 0 || _nonExistPartNo != "")
                //    return $"{_nonExistPartNo} 物料編號不存在";

                var _releaseExpendables = _result.Item2.Where(w => w.PartNo.Substring(0, 1) == "C");
                var _releaseComponents = _result.Item2.Where(w => w.PartNo.Substring(0, 1) == "E");
                var _releaseJigs = _result.Item2.Where(w => w.PartNo.Substring(0, 1) == "T");
                var _releaseWirePanel = _result.Item2.Where(w => w.PartNo.Substring(0, 1) == "B");
                var _releaseSample = _result.Item2.Where(w => w.PartNo.Substring(0, 1) == "A");

                if (_releaseExpendables.Any())
                {
                    _tmpResult = _expendablesDomainService.ProcessRelease(_releaseExpendables, entity.CreateUser, entity.CreateDate, entity.Memo, _nowTime, userEntity);
                    _processResult += _tmpResult.Item1;
                    _processCnt += _tmpResult.Item2;
                }

                if (_releaseComponents.Any())
                {
                    _tmpResult = _componentsDomainService.ProcessRelease(_releaseComponents, entity.CreateUser, entity.CreateDate, entity.Memo, _nowTime, userEntity);
                    _processResult += _tmpResult.Item1;
                    _processCnt += _tmpResult.Item2;
                }

                if (_releaseJigs.Any())
                {
                    _tmpResult = _jigsDomainService.ProcessRelease(_releaseJigs, entity.CreateUser, entity.CreateDate, entity.Memo, _nowTime, userEntity);
                    _processResult += _tmpResult.Item1;
                    _processCnt += _tmpResult.Item2;
                }

                if (_releaseWirePanel.Any())
                {
                    _tmpResult = _wirePanelDomainService.ProcessRelease(_releaseWirePanel, entity.CreateUser, entity.CreateDate, entity.Memo, _nowTime, userEntity);
                    _processResult += _tmpResult.Item1;
                    _processCnt += _tmpResult.Item2;
                }

                if (_releaseSample.Any())
                {
                    _tmpResult = _sampleDomainService.ProcessRelease(_releaseSample, entity.CreateUser, entity.CreateDate, entity.Memo, _nowTime, userEntity);
                    _processResult += _tmpResult.Item1;
                    _processCnt += _tmpResult.Item2;
                }

                return _processResult == "" ? "" : $"．成功領用物料項目：{_processCnt} \n\n．領用失敗物料，請負責人員再次確認：\n{_processResult}";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private (string, List<ReleaseGoodsEntity>) Validate(ReleaseEntity entity)
        {
            try
            {
                var _errorInfo = "";

                var _tempList = new List<ReleaseGoodsEntity>
                {
                    new ReleaseGoodsEntity {ColName = "編號 1.", PartNo = entity.PartNo1, Count = entity.Count1 , Node = entity.Node1 ?? ""},
                    new ReleaseGoodsEntity {ColName = "編號 2.", PartNo = entity.PartNo2, Count = entity.Count2 , Node = entity.Node2 ?? ""},
                    new ReleaseGoodsEntity {ColName = "編號 3.", PartNo = entity.PartNo3, Count = entity.Count3 , Node = entity.Node3 ?? ""},
                    new ReleaseGoodsEntity {ColName = "編號 4.", PartNo = entity.PartNo4, Count = entity.Count4 , Node = entity.Node4 ?? ""},
                    new ReleaseGoodsEntity {ColName = "編號 5.", PartNo = entity.PartNo5, Count = entity.Count5 , Node = entity.Node5 ?? ""}
                };

                if (_tempList.Where(a => string.IsNullOrEmpty(a.PartNo)).Count() == 5)
                    return ("無領用物料", null);

                //if (entity.CreateUser == "" || entity.CreateDate == null)
                //    _errorInfo += "領用人員 & 日期 必填 \n";

                var _noPartNo = _tempList.Where(a => string.IsNullOrEmpty(a.PartNo) && a.Count != 0);
                var _noCnt = _tempList.Where(a => !string.IsNullOrEmpty(a.PartNo) && a.Count == 0);

                if (_noPartNo.Any())
                    _noPartNo.ToList().ForEach(fe => _errorInfo += $"{fe.ColName} 物料編號必填\n");
                if (_noCnt.Any())
                    _noCnt.ToList().ForEach(fe => _errorInfo += $"{fe.ColName} 數量必填\n");

                return (_errorInfo, _tempList.Where(w => !string.IsNullOrEmpty(w.PartNo) && w.Count > 0).ToList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
