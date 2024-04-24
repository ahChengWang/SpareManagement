using SpareManagement.DomainService.Entity;
using SpareManagement.Repository;
using System;
using System.Collections.Generic;

namespace SpareManagement.DomainService
{
    public interface IComponentsDomainService
    {
        List<ComponentsEntity> Get(string partNo, string name, string purchaseId);
        (string, int) ProcessRelease(IEnumerable<ReleaseGoodsEntity> releaseGoodsList, string createUser, DateTime? createDate, string memo, DateTime nowTime, UserEntity userEntity);
        string ProcessWarehouse(IEnumerable<BasicInformationDao> basicList, List<WarehouseGoodsEntity> warehouseGoodsList, string createUser, DateTime? createDate, string memo, DateTime nowTime, UserEntity userEntity);
        bool UpdatePlacement(BasicInformationDao updDao);
    }
}