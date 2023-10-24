using SpareManagement.DomainService.Entity;
using System.Collections.Generic;

namespace SpareManagement.DomainService
{
    public interface IInspectDomainService
    {
        List<InspectsEntity> SelectList(int categoryId, string partNo, int isOverdueInspect);
        string Update(InspectsEntity inspectsEntity, UserEntity userEntity);
    }
}