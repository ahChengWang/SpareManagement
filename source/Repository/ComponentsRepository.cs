using SpareManagement.Repository.Dao;
using System;
using System.Collections.Generic;

namespace SpareManagement.Repository
{

    public class ComponentsRepository : BaseRepository, IComponentsRepository
    {

        public List<ComponentsDao> SelectByConditions(IEnumerable<string> partNoList = null,
            string partNo = "",
            string name = "",
            string purchaseId = "")
        {
            var sql = @"select * from Components where 1=1 ";

            if (partNoList != null)
                sql += " and PartNo in @PartNoList ";

            if (partNo != "")
                sql += $" and PartNo like '%{partNo}%' ";

            if (name != "")
                sql += $" and Name like '%{name}%' ";

            if (purchaseId != "")
                sql += $" and PurchaseId like '%{purchaseId}%' ";


            var _result = _dbHelper.ExecuteQuery<ComponentsDao>(sql, new
            {
                PartNoList = partNoList,
                PartNo = partNo,
                Name = name,
                PurchaseId = purchaseId
            });

            return _result;
        }

        public int Insert(List<ComponentsDao> dao)
        {

            // 測試取得資料庫資料
            var sql = @"
INSERT INTO [dbo].[Components]
           ([PartNo]
           ,[Name]
           ,[PurchaseId]
           ,[Spec]
           ,[IsKeySpare]
           ,[IsCommercial]
           ,[Equipment]
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
           ,@Equipment
           ,@Placement
           ,@Inventory
           ,@SafetyCount
           ,@Memo)
";

            var _result = _dbHelper.ExecuteNonQuery(sql, dao);

            return _result;
        }


        public int Update(List<ComponentsDao> dao)
        {

            // 測試取得資料庫資料
            var sql = @"
UPDATE [dbo].[Components]
     SET [Inventory] = @Inventory
     WHERE [PartNo] = @PartNo
";

            var _result = _dbHelper.ExecuteNonQuery(sql, dao);

            return _result;
        }

        public int UpdatePlacement(string partNo, string updPlacement, int saftyCnt)
        {
            try
            {
                var sql = @"
UPDATE [dbo].[Components]
     SET [Placement] = @Placement,
         [SafetyCount] = @SafetyCount
     WHERE [PartNo] = @PartNo
";

                var _result = _dbHelper.ExecuteNonQuery(sql, new
                {
                    Placement = updPlacement,
                    SafetyCount = saftyCnt,
                    PartNo = partNo
                });

                return _result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }


}
