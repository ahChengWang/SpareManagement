using SpareManagement.Repository.Dao;
using System.Collections.Generic;

namespace SpareManagement.Repository
{
    public interface IComponentsRepository
    {
        int Insert(List<ComponentsDao> dao);
        List<ComponentsDao> SelectByConditions(IEnumerable<string> partNoList = null, string partNo = "", string name = "", string purchaseId = "");
        int Update(List<ComponentsDao> dao);
        int UpdatePlacement(string partNo, string updPlacement, int saftyCnt);
    }
}