using SpareManagement.DomainService.Entity;
using System.Collections.Generic;

namespace SpareManagement.DomainService
{
    public interface IFixDomainService
    {
        List<InspectsEntity> SelectList(int categoryId, string partNo);
        string Update(InspectsEntity inspectsEntity, UserEntity userEntity);
    }
}