using SpareManagement.Enum;
using System;

namespace SpareManagement.DomainService.Entity
{
    public class AccountEntity
    {
        public bool IsValidate { get; set; }
        public string Message { get; set; }
        public AccountInfoEntity AccountInfo { get; set; }

    }


    public class AccountInfoEntity
    {
        public string Account { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }

    }
}
