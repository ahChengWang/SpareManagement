using SpareManagement.Repository.Dao;
using System.Collections.Generic;

namespace SpareManagement.Repository
{
    public interface IHistoryRepository
    {
        int Insert(List<HistoryDao> historyDao);
        List<HistoryDao> SelectByConditions(int categoryId, string partNo);
    }
}