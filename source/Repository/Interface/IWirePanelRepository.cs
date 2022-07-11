using SpareManagement.Repository.Dao;
using System;
using System.Collections.Generic;

namespace SpareManagement.Repository
{
    public interface IWirePanelRepository
    {
        int Insert(List<WirePanelDao> dao);
        List<WirePanelDao> SelectByConditions(IEnumerable<string> partNoList = null, string partNo = "", string serialNo = "", string name = "", string purchaseId = "", IEnumerable<string> serialNoList = null, int statusId = 0, DateTime? beginInspectDate = null, DateTime? endInspectDate = null, bool isForMainPage = false);
        int Update(List<WirePanelDao> dao);
        int UpdateRelease(List<WirePanelDao> dao);
        int UpdateStatus(WirePanelDao dao);
    }
}