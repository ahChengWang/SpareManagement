using Helper;
using SpareManagement.DomainService.Entity;
using SpareManagement.Repository.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpareManagement.Repository
{

    public class AccountRepository : BaseRepository
    {

        public List<AccountInfoDao> SelectByConditions(string accountId)
        {
            var sql = @"select * from Account_Info where Account = @Account ";


            var _result = _dbHelper.ExecuteQuery<AccountInfoDao>(sql, new
            {
                Account = accountId
            });

            return _result;
        }


    }


}
