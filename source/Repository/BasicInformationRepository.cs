﻿using SpareManagement.Repository.Dao;
using System;
using System.Collections.Generic;

namespace SpareManagement.Repository
{

    public class BasicInformationRepository : BaseRepository, IBasicInformationRepository
    {

        public List<BasicInformationDao> SelectByConditions(List<string> partNoList = null,
            string partNo = "", 
            string name = "", 
            string purchaseId = "", 
            string placement = "", 
            DateTime? createStart = null, 
            DateTime? createEnd = null,
            int categoryId = 0)
        {
            try
            {
                var sql = @"select * from Basic_information where 1=1 ";

                if (categoryId != 0)
                    sql += " and CategoryId=@CategoryId ";
                if (partNoList != null)
                    sql += " and PartNo in @PartNo ";
                if (partNo != "")
                    sql += $" and PartNo like '%{partNo}%' ";
                if (name != "")
                    sql += $" and Name like '%{name}%' ";
                if (purchaseId != "")
                    sql += $" and PurchaseId like '%{purchaseId}%' ";
                if (placement != "")
                    sql += $" and Placement like '%{placement}%' ";
                if (createStart != null && createEnd != null)
                    sql += $" and CreateDate between @createStart and @createEnd ";


                var _result = _dbHelper.ExecuteQuery<BasicInformationDao>(sql, new
                {
                    CategoryId = categoryId,
                    PartNo = partNoList,
                    createStart = createStart,
                    createEnd = createEnd
                });

                return _result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public int GetBasicInfoCategoryIdCount(int categoryId)
        {
            try
            {
                var sql = @"select top 1 CONVERT(int,SUBSTRING(PartNo,3,LEN(PartNo))) from Basic_information where CategoryId = @CategoryId order by PartNo desc; ";

                var _result = _dbHelper.ExecuteScalar<int>(sql, new
                {
                    CategoryId = categoryId
                });

                return _result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public int UpdateLastSerialNo(List<BasicInformationDao> updateSerialNoList)
        {
            try
            {
                var sql = @"
BEGIN TRAN  
update Basic_information set LastSerialNo = @LastSerialNo where CategoryId = @CategoryId and PartNo = @PartNo ; 
COMMIT TRAN; 
";
                var _result = _dbHelper.ExecuteNonQuery(sql, updateSerialNoList);

                return _result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int Insert(BasicInformationDao basicDao)
        {
            try
            {// 測試取得資料庫資料
                var sql = @" 
BEGIN TRAN  

INSERT INTO [dbo].[Basic_information]
           ([CategoryId]
           ,[Name]
           ,[PartNo]
           ,[Spec]
           ,[PurchaseId]
           ,[IsKeySpare]
           ,[IsCommercial]
           ,[Equipment]
           ,[UseLocation]
           ,[PurchaseDelivery]
           ,[SafetyCount]
           ,[IsSpecial]
           ,[IsNeedInspect]
           ,[InspectCycle]
           ,[Placement]
           ,[CreateUser]
           ,[CreateDate]
           ,[Manager]
           ,[Memo])
     VALUES
           (@CategoryId
           ,@Name
           ,@PartNo
           ,@Spec
           ,@PurchaseId
           ,@IsKeySpare
           ,@IsCommercial
           ,@Equipment
           ,@UseLocation
           ,@PurchaseDelivery
           ,@SafetyCount
           ,@IsSpecial
           ,@IsNeedInspect
           ,@InspectCycle
           ,@Placement
           ,@CreateUser
           ,@CreateDate
           ,@Manager
           ,@Memo); 
COMMIT TRAN;   
";

                var _result = _dbHelper.ExecuteNonQuery(sql, basicDao);

                return _result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public int Update(BasicInformationDao updDao)
        {
            try
            {
                var sql = @"
BEGIN TRAN  
update Basic_information set PurchaseId = @PurchaseId,Placement = @Placement,SafetyCount=@SafetyCount where PartNo = @PartNo ;  
COMMIT TRAN;   
";

                var _result = _dbHelper.ExecuteNonQuery(sql, new {
                    PartNo = updDao.PartNo,
                    PurchaseId = updDao.PurchaseId,
                    Placement = updDao.Placement,
                    SafetyCount = updDao.SafetyCount
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
