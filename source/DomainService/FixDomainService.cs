﻿using SpareManagement.DomainService.Entity;
using SpareManagement.Enum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpareManagement.DomainService
{
    public class FixDomainService : IFixDomainService
    {
        private readonly IJigsDomainService _jigsDomainService;
        private readonly IWirePanelDomainService _wirePanelDomainService;
        private readonly ISampleDomainService _sampleDomainService;

        public FixDomainService(IJigsDomainService jigsDomainService,
            IWirePanelDomainService wirePanelDomainService,
            ISampleDomainService sampleDomainService)
        {
            _jigsDomainService = jigsDomainService;
            _wirePanelDomainService = wirePanelDomainService;
            _sampleDomainService = sampleDomainService;
        }

        public List<InspectsEntity> SelectList(int categoryId, string partNo)
        {
            try
            {
                var _inspectsEntity = new List<InspectsEntity>();
                DateTime? beginDTE = null;
                DateTime? endDTE = null;

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
                                SerialNo = fe.SerialNo
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
                                SerialNo = fe.SerialNo
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
                                SerialNo = fe.SerialNo
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
                var _updResult = "";
                DateTime _nowTime = DateTime.Now;

                switch (inspectsEntity.CategoryId)
                {
                    case 3:
                        _updResult =
                            _jigsDomainService.Update(
                                inspectsEntity.SerialNo,
                                StatusEnum.Stock,
                                StatusEnum.Fixing,
                                inspectsEntity.UpdateUser,
                                inspectsEntity.UpdateDate,
                                userEntity,
                                _nowTime,
                                errSummary: "維修");
                        break;
                    case 4:
                        _updResult =
                            _wirePanelDomainService.Update(
                                inspectsEntity.SerialNo,
                                StatusEnum.Stock,
                                StatusEnum.Fixing,
                                inspectsEntity.UpdateUser,
                                inspectsEntity.UpdateDate,
                                userEntity,
                                _nowTime,
                                errSummary: "維修");
                        break;
                    case 5:
                        _updResult =
                            _sampleDomainService.Update(
                                inspectsEntity.SerialNo,
                                StatusEnum.Stock,
                                StatusEnum.Fixing,
                                inspectsEntity.UpdateUser,
                                inspectsEntity.UpdateDate,
                                userEntity,
                                _nowTime,
                                errSummary: "維修");
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
