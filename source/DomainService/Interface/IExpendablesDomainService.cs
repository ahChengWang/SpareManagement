﻿using SpareManagement.DomainService.Entity;
using SpareManagement.Repository;
using System;
using System.Collections.Generic;

namespace SpareManagement.DomainService
{
    public interface IExpendablesDomainService
    {
        List<ExpendablesEntity> Get(string partNo, string name, string purchaseId);
        (string, int) ProcessRelease(IEnumerable<ReleaseGoodsEntity> releaseGoodsList, string createUser, DateTime createDate, string memo);
        string ProcessWarehouse(IEnumerable<BasicInformationDao> basicList, List<WarehouseGoodsEntity> warehouseGoodsList, string createUser, DateTime createDate, string memo);
    }
}