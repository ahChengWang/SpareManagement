using SpareManagement.DomainService.Entity;
using SpareManagement.Enum;
using SpareManagement.Helper;
using SpareManagement.Repository;
using SpareManagement.Repository.Dao;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpareManagement.DomainService
{
    public class InspectDomainService : IInspectDomainService
    {
        private readonly IBasicInformationDomainService _basicInformationDomainService;
        private readonly IJigsDomainService _jigsDomainService;
        private readonly IWirePanelDomainService _wirePanelDomainService;
        private readonly ISampleDomainService _sampleDomainService;

        public InspectDomainService(IBasicInformationDomainService basicInformationDomainService,
            IJigsDomainService jigsDomainService,
            IWirePanelDomainService wirePanelDomainService,
            ISampleDomainService sampleDomainService)
        {
            _basicInformationDomainService = basicInformationDomainService;
            _jigsDomainService = jigsDomainService;
            _wirePanelDomainService = wirePanelDomainService;
            _sampleDomainService = sampleDomainService;
        }

        public List<InspectsEntity> SelectList(int categoryId, string partNo, int isOverdueInspect)
        {
            try
            {
                var _inspectsEntity = new List<InspectsEntity>();
                DateTime? beginDTE = null;
                DateTime? endDTE = null;

                if (isOverdueInspect == 1)
                    endDTE = DateTime.Now.AddSeconds(-1);
                else
                    beginDTE = DateTime.Now;

                switch (categoryId)
                {
                    case 3:
                        var _jigsInspectData = _jigsDomainService.GetDetail(
                            null,
                            serialNo: partNo,
                            statusId: (int)StatusEnum.Stock,
                            beginInspectDate: beginDTE,
                            endInspectDate: endDTE,
                            isForMainPage: true);

                        _jigsInspectData.ForEach(fe =>
                        {
                            _inspectsEntity.Add(new InspectsEntity
                            {
                                PartNo = fe.PartNo,
                                Name = fe.Name,
                                Spec = fe.Spec,
                                SerialNo = fe.SerialNo,
                                NextInspectDate = (DateTime)fe.NextInspectDate
                            });
                        });
                        break;
                    case 4:
                        var _wirePanelInspectData = _wirePanelDomainService.GetDetail(
                            null,
                            serialNo: partNo,
                            statusId: (int)StatusEnum.Stock,
                            beginInspectDate: beginDTE,
                            endInspectDate: endDTE,
                            isForMainPage: true);

                        _wirePanelInspectData.ForEach(fe =>
                        {
                            _inspectsEntity.Add(new InspectsEntity
                            {
                                PartNo = fe.PartNo,
                                Name = fe.Name,
                                Spec = fe.Spec,
                                SerialNo = fe.SerialNo,
                                NextInspectDate = (DateTime)fe.NextInspectDate
                            });
                        });
                        break;
                    case 5:
                        var _sampleInspectData = _sampleDomainService.GetDetail(
                            null,
                            serialNo: partNo,
                            statusId: (int)StatusEnum.Stock,
                            beginInspectDate: beginDTE,
                            endInspectDate: endDTE,
                            isForMainPage: true);

                        _sampleInspectData.ForEach(fe =>
                        {
                            _inspectsEntity.Add(new InspectsEntity
                            {
                                PartNo = fe.PartNo,
                                Name = fe.Name,
                                Spec = fe.Spec,
                                SerialNo = fe.SerialNo,
                                NextInspectDate = (DateTime)fe.NextInspectDate
                            });
                        });
                        break;
                }

                return _inspectsEntity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Update(InspectsEntity inspectsEntity, UserEntity userEntity)
        {
            try
            {
                var _basicInfo = _basicInformationDomainService.GetInspectInfo(new BasicInfoEntity { PartNo = inspectsEntity.PartNo });
                var _updResult = "";
                DateTime _nowTime = DateTime.Now;

                switch (inspectsEntity.CategoryId)
                {
                    case 3:
                        _updResult =
                            _jigsDomainService.Update(
                                inspectsEntity.SerialNo,
                                StatusEnum.Stock,
                                StatusEnum.Inspecting,
                                inspectsEntity.UpdateUser,
                                inspectsEntity.UpdateDate,
                                userEntity,
                                _nowTime,
                                inspectCycle: _basicInfo.InspectCycle,
                                errSummary: "檢驗");
                        break;
                    case 4:
                        _updResult =
                            _wirePanelDomainService.Update(
                                inspectsEntity.SerialNo,
                                StatusEnum.Stock,
                                StatusEnum.Inspecting,
                                inspectsEntity.UpdateUser,
                                inspectsEntity.UpdateDate,
                                userEntity,
                                _nowTime,
                                inspectCycle: _basicInfo.InspectCycle,
                                errSummary: "檢驗");
                        break;
                    case 5:
                        _updResult =
                            _sampleDomainService.Update(
                                inspectsEntity.SerialNo,
                                StatusEnum.Stock,
                                StatusEnum.Inspecting,
                                inspectsEntity.UpdateUser,
                                inspectsEntity.UpdateDate,
                                userEntity,
                                _nowTime,
                                inspectCycle: _basicInfo.InspectCycle,
                                errSummary: "檢驗");
                        break;
                }

                return _updResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
