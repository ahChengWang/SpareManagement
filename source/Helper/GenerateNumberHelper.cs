using SpareManagement.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpareManagement.Helper
{
    public class GenerateNumberHelper
    {
        private readonly SerialNumberRepository _serialNumberRepository;

        public GenerateNumberHelper(SerialNumberRepository serialNumberRepository)
        {
            _serialNumberRepository = serialNumberRepository;
        }


        public string Generate(int categoryId)
        {
            var _serialNumber = _serialNumberRepository.GetNumber(categoryId);

            switch (categoryId)
            {
                case 1:
                    return $"C_{(_serialNumber.LastMaterialNo + 1).ToString("000000")}";
                case 2:
                    return $"E_{(_serialNumber.LastMaterialNo + 1).ToString("000000")}";
                case 3:
                    return $"T_{(_serialNumber.LastMaterialNo + 1).ToString("000000")}_{(_serialNumber.LastSerialNo + 1).ToString("000000")}";
                case 4:
                    return $"B_{(_serialNumber.LastMaterialNo + 1).ToString("000000")}_{(_serialNumber.LastSerialNo + 1).ToString("000000")}";
                default:
                    return $"X_{(_serialNumber.LastMaterialNo + 1).ToString("000000")}_{(_serialNumber.LastSerialNo + 1).ToString("000000")}";
            }
        }
    }
}
