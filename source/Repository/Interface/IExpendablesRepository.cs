using SpareManagement.Repository.Dao;
using System.Collections.Generic;

namespace SpareManagement.Repository
{
    public interface IExpendablesRepository
    {
        int Insert(List<ExpendablesDao> dao);
        List<ExpendablesDao> SelectByConditions(IEnumerable<string> partNoList = null, string partNo = "", string name = "", string purchaseId = "");
        int Update(List<ExpendablesDao> dao);
    }
}