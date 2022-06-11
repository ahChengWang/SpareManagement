using Helper;
using SpareManagement.DomainService.Entity;
using SpareManagement.Repository.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpareManagement.Repository
{

    public class HistoryRepository : BaseRepository
    {

        public List<HistoryDao> SelectByConditions(int categoryId, string partNo)
        {
            var sql = @"select * from History where CategoryId = @CategoryId and PartNo = @PartNo order by id asc ";


            var _result = _dbHelper.ExecuteQuery<HistoryDao>(sql, new
            {
                CategoryId = categoryId,
                PartNo = partNo
            });

            return _result;
        }


        public int Insert(List<HistoryDao> historyDao)
        {

            // 測試取得資料庫資料
            var sql = @"
INSERT INTO [dbo].[History]
           ([CategoryId]
           ,[PartNo]
           ,[Status]
           ,[Quantity]
           ,[EmpName]
           ,[UpdateTime]
           ,[Memo])
     VALUES
           (@CategoryId
           ,@PartNo
           ,@Status
           ,@Quantity
           ,@EmpName
           ,@UpdateTime
           ,@Memo)
";

            var _result = _dbHelper.ExecuteNonQuery(sql, historyDao);

            return _result;
        }
    }


}
