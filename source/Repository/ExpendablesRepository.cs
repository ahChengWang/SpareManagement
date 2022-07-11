using SpareManagement.Enum;
using SpareManagement.Repository.Dao;
using System.Collections.Generic;

namespace SpareManagement.Repository
{

    public class ExpendablesRepository : BaseRepository, IExpendablesRepository
    {

        public List<ExpendablesDao> SelectByConditions(IEnumerable<string> partNoList = null,
            string partNo = "",
            string name = "",
            string purchaseId = "")
        {
            var sql = @"select * from Expendables where 1=1 ";

            if (partNoList != null)
                sql += " and PartNo in @PartNoList ";

            if (partNo != "")
                sql += $" and PartNo like '%{partNo}%' ";

            if (name != "")
                sql += $" and Name like '%{name}%' ";

            if (purchaseId != "")
                sql += $" and PurchaseId like '%{purchaseId}%' ";


            var _result = _dbHelper.ExecuteQuery<ExpendablesDao>(sql, new
            {
                PartNoList = partNoList,
                PartNo = partNo,
                Name = name,
                PurchaseId = purchaseId
            });

            return _result;
        }

        public int Insert(List<ExpendablesDao> dao)
        {

            // 測試取得資料庫資料
            var sql = @"
INSERT INTO [dbo].[Expendables]
           ([PartNo]
           ,[Name]
           ,[PurchaseId]
           ,[Spec]
           ,[IsKeySpare]
           ,[IsCommercial]
           ,[Placement]
           ,[Inventory]
           ,[SafetyCount]
           ,[Memo])
     VALUES
           (@PartNo
           ,@Name
           ,@PurchaseId
           ,@Spec
           ,@IsKeySpare
           ,@IsCommercial
           ,@Placement
           ,@Inventory
           ,@SafetyCount
           ,@Memo)
";

            var _result = _dbHelper.ExecuteNonQuery(sql, dao);

            return _result;
        }


        public int Update(List<ExpendablesDao> dao)
        {

            // 測試取得資料庫資料
            var sql = @"
UPDATE [dbo].[Expendables]
     SET [Inventory] = @Inventory
     WHERE [PartNo] = @PartNo
";

            var _result = _dbHelper.ExecuteNonQuery(sql, dao);

            return _result;
        }
    }


}
