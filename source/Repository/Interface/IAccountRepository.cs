using SpareManagement.Repository.Dao;
using System.Collections.Generic;

namespace SpareManagement.Repository
{
    public interface IAccountRepository
    {
        List<AccountInfoDao> SelectByConditions(string accountId);
    }
}