using SpareManagement.DomainService.Entity;
using System.Collections.Generic;

namespace SpareManagement.DomainService
{
    public interface IHistoryDomainService
    {
        List<HistoryEntity> GetHistory(int categoryId, string partNo);
        int Insert(List<HistoryEntity> historyList);
    }
}