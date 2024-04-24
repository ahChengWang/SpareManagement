using SpareManagement.Repository.Dao;
using System;
using System.Collections.Generic;

namespace SpareManagement.Repository
{
    public interface ISampleRepository
    {
        int Insert(List<SampleDao> dao);
        List<SampleDao> SelectByConditions(IEnumerable<string> partNoList = null, string partNo = "", string serialNo = "", string name = "", string purchaseId = "", IEnumerable<string> serialNoList = null, int statusId = 0, DateTime? beginInspectDate = null, DateTime? endInspectDate = null, bool isForMainPage = false);
        List<SampleDao> SelectBySerialNo(string serialNo = "");
        int Update(List<SampleDao> dao);
        int UpdateRelease(List<SampleDao> dao);
        int UpdateStatus(SampleDao dao);
        int UpdatePlacement(string partNo, string updPlacement, int saftyCnt, string purchaseId);
    }
}