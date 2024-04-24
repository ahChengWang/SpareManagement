using SpareManagement.Repository.Dao;
using System;
using System.Collections.Generic;

namespace SpareManagement.Repository
{
    public interface IJigsRepository
    {
        int Insert(List<JigsDao> dao);
        List<JigsDao> SelectByConditions(IEnumerable<string> partNoList = null, string partNo = "", string serialNo = "", string name = "", string purchaseId = "", IEnumerable<string> serialNoList = null, int statusId = 0, DateTime? beginInspectDate = null, DateTime? endInspectDate = null, bool isForMainPage = false);
        List<JigsDao> SelectBySerialNo(string serialNo = "");
        int Update(List<JigsDao> dao);
        int UpdateRelease(List<JigsDao> dao);
        int UpdateStatus(JigsDao dao);
        int UpdatePlacement(string partNo, string updPlacement, int saftyCnt, string purchaseId);
    }
}