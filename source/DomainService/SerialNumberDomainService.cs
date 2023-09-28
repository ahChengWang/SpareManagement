using SpareManagement.Repository;

namespace SpareManagement.DomainService
{
    public class SerialNumberDomainService : ISerialNumberDomainService
    {
        public string GeneratePartNo(int categoryId, int categoryCnt)
        {
            //var _serialNumber = _serialNumberRepository.GetNumber(categoryId);

            switch (categoryId)
            {
                case 1:
                    return $"C_{categoryCnt + 1:0000}";
                case 2:
                    return $"E_{categoryCnt + 1:0000}";
                case 3:
                    return $"T_{categoryCnt + 1:0000}";
                case 4:
                    return $"B_{categoryCnt + 1:0000}";
                case 5:
                    return $"A_{categoryCnt + 1:00000}";
                default:
                    return $"X_{categoryCnt + 1:0000}";
            }
        }
    }
}
