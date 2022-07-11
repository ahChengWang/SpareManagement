using SpareManagement.Repository.Dao;

namespace SpareManagement.Repository
{
    public interface ISerialNumberRepository
    {
        SerialNumberDao GetNumber(int categoryId);
        int UpdateNumber(int categoryId, int increaseCnt, bool isForWarehouse);
    }
}