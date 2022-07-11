using SpareManagement.DomainService.Entity;
using System.Collections.Generic;

namespace SpareManagement.DomainService
{
    public interface IReturnDomainService
    {
        List<InspectsEntity> SelectList(int categoryId, string partNo, int statusId);
        string Update(InspectsEntity inspectsEntity);
    }
}