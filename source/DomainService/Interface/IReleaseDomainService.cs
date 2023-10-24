using SpareManagement.DomainService.Entity;

namespace SpareManagement.DomainService
{
    public interface IReleaseDomainService
    {
        string Release(ReleaseEntity entity, UserEntity userEntity);
    }
}