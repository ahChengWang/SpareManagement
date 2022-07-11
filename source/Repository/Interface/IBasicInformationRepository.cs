using SpareManagement.Repository.Dao;
using System;
using System.Collections.Generic;

namespace SpareManagement.Repository
{
    public interface IBasicInformationRepository
    {
        int GetBasicInfoCategoryIdCount(int categoryId);
        int Insert(BasicInformationDao basicDao);
        List<BasicInformationDao> SelectByConditions(List<string> partNoList = null, string partNo = "", string name = "", string purchaseId = "", string placement = "", DateTime? createStart = null, DateTime? createEnd = null);
        int UpdateLastSerialNo(List<BasicInformationDao> updateSerialNoList);
    }
}