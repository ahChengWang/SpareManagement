using SpareManagement.DomainService.Entity;
using SpareManagement.Enum;
using SpareManagement.Repository;
using System;
using System.Collections.Generic;

namespace SpareManagement.DomainService
{
    public interface ISampleDomainService
    {
        List<SampleEntity> Get(string partNo, string name, string purchaseId);
        List<SampleEntity> GetDetail(string partNo, string name = "", string purchaseId = "", string serialNo = "", IEnumerable<string> serialNoList = null, int statusId = 0, DateTime? beginInspectDate = null, DateTime? endInspectDate = null, bool isForMainPage = false, List<string> partNoList = null);
        SampleEntity GetEditData(string partNo, string serialNo);
        (string, int) ProcessRelease(IEnumerable<ReleaseGoodsEntity> releaseJigsList, string createUser, DateTime createDate, string memo);
        string ProcessWarehouse(IEnumerable<BasicInformationDao> basicList, List<WarehouseGoodsEntity> warehouseGoodsList, string createUser, DateTime createDate, string memo);
        string Update(string serialNo, StatusEnum oldStatusId, StatusEnum newStatusId, string updateUser, DateTime updateDTE, int? inspectCycle = null, string errSummary = "");
        string UpdateInspectAndTemporary(string partNo, string serialNo, string inspectDate, bool temporary, string placement);
    }
}