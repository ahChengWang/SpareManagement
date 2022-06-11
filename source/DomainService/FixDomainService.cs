using SpareManagement.DomainService.Entity;
using SpareManagement.Enum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpareManagement.DomainService
{
    public class FixDomainService
    {
        private readonly JigsDomainService _jigsDomainService;
        private readonly WirePanelDomainService _wirePanelDomainService;

        public FixDomainService(JigsDomainService jigsDomainService,
            WirePanelDomainService wirePanelDomainService)
        {
            _jigsDomainService = jigsDomainService;
            _wirePanelDomainService = wirePanelDomainService;
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
                                StatusEnum.Stock,
                                StatusEnum.Fixing,
                                inspectsEntity.UpdateUser,
                                inspectsEntity.UpdateDate,
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
