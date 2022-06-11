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
    public class ReturnDomainService
    {
        private readonly JigsDomainService _jigsDomainService;
        private readonly WirePanelDomainService _wirePanelDomainService;

        public ReturnDomainService(JigsDomainService jigsDomainService,
            WirePanelDomainService wirePanelDomainService)
        {
            _jigsDomainService = jigsDomainService;
            _wirePanelDomainService = wirePanelDomainService;
        }

        public List<InspectsEntity> SelectList(int categoryId, string partNo, int statusId)
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
                            statusId: statusId,
                            beginInspectDate: beginDTE,
                            endInspectDate: endDTE,
                            isForMainPage: true);

                        _jigsInspectData.Where(w => w.Status != StatusEnum.Stock).ToList().ForEach(fe =>
                        {
                            _inspectsEntity.Add(new InspectsEntity
                            {
                                PartNo = fe.PartNo,
                                Name = fe.Name,
                                Spec = fe.Spec,
                                SerialNo = fe.SerialNo,
                                Status = fe.Status
                            });
                        });
                        break;
                    case 4:
                        var _wirePanelInspectData = _wirePanelDomainService.GetDetail(
                            null,
                            serialNo: partNo,
                            statusId: statusId,
                            beginInspectDate: beginDTE,
                            endInspectDate: endDTE,
                            isForMainPage: true);

                        _wirePanelInspectData.Where(w => w.Status != StatusEnum.Stock).ToList().ForEach(fe =>
                        {
                            _inspectsEntity.Add(new InspectsEntity
                            {
                                PartNo = fe.PartNo,
                                Name = fe.Name,
                                Spec = fe.Spec,
                                SerialNo = fe.SerialNo,
                                Status = fe.Status
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

        public string Update(InspectsEntity inspectsEntity)
        {
            try
            {
                var _updResult = "";

                switch (inspectsEntity.CategoryId)
                {
                    case 3:
                        _updResult =
                            _jigsDomainService.Update(
                                inspectsEntity.SerialNo,
                                inspectsEntity.Status,
                                StatusEnum.Stock,
                                inspectsEntity.UpdateUser,
                                inspectsEntity.UpdateDate,
                                errSummary: "歸還");
                        break;
                    case 4:
                        _updResult =
                            _wirePanelDomainService.Update(
                                inspectsEntity.SerialNo,
                                inspectsEntity.Status,
                                StatusEnum.Stock,
                                inspectsEntity.UpdateUser,
                                inspectsEntity.UpdateDate,
                                errSummary: "歸還");
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
