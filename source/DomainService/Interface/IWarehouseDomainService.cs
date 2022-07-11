using SpareManagement.DomainService.Entity;

namespace SpareManagement.DomainService
{
    public interface IWarehouseDomainService
    {
        string Insert(WarehouseInsertEntity entity);
    }
}