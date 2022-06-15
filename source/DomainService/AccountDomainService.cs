using SpareManagement.DomainService.Entity;
using SpareManagement.Repository;
using System.Linq;

namespace SpareManagement.DomainService
{
    public class AccountDomainService
    {
        private readonly AccountRepository _accountRepository;

        public AccountDomainService(AccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public AccountEntity Validate(AccountEntity loginentity)
        {
            if (loginentity.AccountInfo.Account.Trim() == "")
            {
                return new AccountEntity { IsValidate = false, Message = "帳號錯誤" };
            }

            var _accInfo = _accountRepository.SelectByConditions(loginentity.AccountInfo.Account).FirstOrDefault();

            if (_accInfo == null)
            {
                return new AccountEntity { IsValidate = false, Message = "查無此帳號" };
            }

            if (_accInfo.Password != loginentity.AccountInfo.Password)
            {
                return new AccountEntity { IsValidate = false, Message = "密碼錯誤" };
            }

            return new AccountEntity
            {
                IsValidate = true,
                Message = "",
                AccountInfo = new AccountInfoEntity { Account = _accInfo.Account.ToString(), Password = _accInfo.Password, Name = _accInfo.Name }
            };
        }

    }
}
