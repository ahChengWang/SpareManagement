using SpareManagement.DomainService.Entity;

namespace SpareManagement.DomainService
{
    public interface IAccountDomainService
    {
        AccountEntity Validate(AccountEntity loginentity);
    }
}