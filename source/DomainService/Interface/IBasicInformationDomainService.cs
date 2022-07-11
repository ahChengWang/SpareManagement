using SpareManagement.DomainService.Entity;
using System.Collections.Generic;

namespace SpareManagement.DomainService
{
    public interface IBasicInformationDomainService
    {
        string Create(BasicInfoEntity basicEntity);
        BasicInfoEntity GetInspectInfo(BasicInfoEntity basicEntity);
        List<BasicInfoEntity> Select(string partNo, string name, string purchaseId, string placement, string createTime);
    }
}