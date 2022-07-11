namespace SpareManagement.DomainService
{
    public interface ISerialNumberDomainService
    {
        string GeneratePartNo(int categoryId, int categoryCnt);
    }
}