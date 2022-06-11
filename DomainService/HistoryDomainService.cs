using SpareManagement.DomainService.Entity;
using SpareManagement.Helper;
using SpareManagement.Repository;
using SpareManagement.Repository.Dao;
using System.Collections.Generic;

namespace SpareManagement.DomainService
{
    public class HistoryDomainService
    {
        private readonly HistoryRepository _historyRepository;

        public HistoryDomainService(HistoryRepository historyRepository)
        {
            _historyRepository = historyRepository;
        }

        public List<HistoryEntity> GetHistory(int categoryId, string partNo)
        {
            var result = _historyRepository.SelectByConditions(categoryId, partNo);

            return result.CopyAToB<HistoryEntity>();
        }

        public int Insert(List<HistoryEntity> historyList)
        {
            var _insHistoryList = historyList.CopyAToB<HistoryDao>();

            var _insCnt = _historyRepository.Insert(_insHistoryList);

            return _insCnt;
        }
    }
}
