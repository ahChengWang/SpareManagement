﻿using SpareManagement.Enum;
using SpareManagement.Repository.Dao;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpareManagement.Repository
{

    public class JigsRepository : BaseRepository
    {
        public List<JigsDao> SelectByConditions(IEnumerable<string> partNoList = null,
            string partNo = "",
            string serialNo = "",
            string name = "",
            string purchaseId = "",
            IEnumerable<string> serialNoList = null,
            int statusId = 0,
            DateTime? beginInspectDate = null,
            DateTime? endInspectDate = null,
            bool isForMainPage = false)
        {
            var sql = @"select * from Jigs where 1=1 ";

            if (partNoList != null && partNoList.Any())
                sql += " and PartNo in @PartNoList ";

            if (partNo != "")
                sql += $" and PartNo like '%{partNo}%' ";

            if (isForMainPage && serialNo != "")
                sql += $" and SerialNo like '%{serialNo}%' ";
            else if (serialNo != "")
                sql += " and SerialNo = @SerialNo ";

            if (serialNoList != null)
                sql += " and SerialNo in @SerialNoList ";

            if (name != "")
                sql += $" and Name like '%{name}%' ";

            if (purchaseId != "")
                sql += $" and PurchaseId like '%{purchaseId}%' ";

            if (statusId != 0)
                sql += $" and Status = @Status ";

            if (beginInspectDate != null)
                sql += $" and NextInspectDate >= @BeginInspectDate ";

            if (endInspectDate != null)
                sql += $" and NextInspectDate < @EndInspectDate ";

            var _result = _dbHelper.ExecuteQuery<JigsDao>(sql, new
            {
                PartNoList = partNoList,
                PartNo = partNo,
                SerialNo = serialNo,
                Name = name,
                PurchaseId = purchaseId,
                SerialNoList = serialNoList,
                Status = statusId,
                BeginInspectDate = beginInspectDate,
                EndInspectDate = endInspectDate
            });

            return _result;
        }

        public List<JigsDao> SelectBySerialNo(string serialNo = "")
        {
            var sql = $"select * from Jigs where SerialNo like '%{serialNo}%' ";

            var _result = _dbHelper.ExecuteQuery<JigsDao>(sql);

            return _result;
        }

        public int Insert(List<JigsDao> dao)
        {

            // 測試取得資料庫資料
            var sql = @"
INSERT INTO [dbo].[Jigs]
           ([PartNo]
           ,[Name]
           ,[SerialNo]
           ,[Status]
           ,[PurchaseId]
           ,[Spec]
           ,[IsKeySpare]
           ,[IsCommercial]
           ,[Equipment]
           ,[Placement]
           ,[Inventory]
           ,[SafetyCount]
           ,[IsNeedInspect]
           ,[InspectDate]
           ,[NextInspectDate]
           ,[IsOverdueInspect]
           ,[IsTemporary]
           ,[Memo])
     VALUES
           (@PartNo
           ,@Name
           ,@SerialNo
           ,@Status
           ,@PurchaseId
           ,@Spec
           ,@IsKeySpare
           ,@IsCommercial
           ,@Equipment
           ,@Placement
           ,@Inventory
           ,@SafetyCount
           ,@IsNeedInspect
           ,@InspectDate
           ,@NextInspectDate
           ,@IsOverdueInspect
           ,@IsTemporary
           ,@Memo)
";

            var _result = _dbHelper.ExecuteNonQuery(sql, dao);

            return _result;
        }
        
        public int Update(List<JigsDao> dao)
        {

            // 測試取得資料庫資料
            var sql = @"
UPDATE [dbo].[Jigs]
     SET [InspectDate] = @InspectDate,
         [Placement] = @Placement,
         [IsTemporary] = @IsTemporary,
         [NextInspectDate] = @NextInspectDate,
         [IsOverdueInspect] = @IsOverdueInspect
     WHERE [SerialNo] = @SerialNo 
";

            var _result = _dbHelper.ExecuteNonQuery(sql, dao);

            return _result;
        }

        public int UpdateStatus(JigsDao dao)
        {
            var sql = @"
UPDATE [dbo].[Jigs]
     SET [Status] = @Status
";
            if (dao.InspectDate != null)
                sql += " , [InspectDate] = @InspectDate ";
            if (dao.NextInspectDate != null)
                sql += " , [NextInspectDate] = @NextInspectDate ";

            sql += " WHERE [SerialNo] = @SerialNo ";

            var _result = _dbHelper.ExecuteNonQuery(sql, new {
                Status = dao.Status,
                InspectDate = dao.InspectDate,
                NextInspectDate = dao.NextInspectDate,
                SerialNo = dao.SerialNo
            });

            return _result;
        }

        public int UpdateRelease(List<JigsDao> dao)
        {

            // 測試取得資料庫資料
            var sql = @"
UPDATE [dbo].[Jigs]
     SET [Status] = @Status,
         [Inventory] = @Inventory
     WHERE [SerialNo] = @SerialNo 
";

            var _result = _dbHelper.ExecuteNonQuery(sql, dao);

            return _result;
        }
    }
}
