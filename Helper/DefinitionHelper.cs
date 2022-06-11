using SpareManagement.Enum;
using SpareManagement.Models;
using SpareManagement.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SpareManagement.Helper
{
    public class DefinitionHelper
    {
        private readonly BasicInformationRepository _basicInformationRepository;

        public DefinitionHelper(BasicInformationRepository basicInformationRepository)
        {
            _basicInformationRepository = basicInformationRepository;
        }

        public static List<SpareType> GetSpareType()
        => new List<SpareType> {
                new SpareType { TypeId = 1, TypeName = "一般耗材"},
                new SpareType { TypeId = 2, TypeName = "設備零件"},
                new SpareType { TypeId = 3, TypeName = "治具"},
                new SpareType { TypeId = 4, TypeName = "線板材"}
        };

        public static List<SpareType> GetNodeList()
        => new List<SpareType> {
                new SpareType { TypeId = 1, TypeName = "Bonding"},
                new SpareType { TypeId = 2, TypeName = "LAM"},
                new SpareType { TypeId = 3, TypeName = "ASSY"},
                new SpareType { TypeId = 4, TypeName = "CDP"}
        };

        public static List<SpareType> GetInspectOption()
        => new List<SpareType> {
                new SpareType { TypeId = 0, TypeName = "否"},
                new SpareType { TypeId = 1, TypeName = "是"}
        };

        public static List<SpareType> GetInspectCategoryOption()
        => new List<SpareType> {
                new SpareType { TypeId = 3, TypeName = "治具"},
                new SpareType { TypeId = 4, TypeName = "線板材"}
        };

        public static List<SpareType> GetReturnStatusOption()
        {
           return GetStatus().Where(w => w.TypeId != 1).ToList();
        }

        //=> new List<SpareType> {
        //        new SpareType { TypeId = 3, TypeName = "治具"},
        //        new SpareType { TypeId = 4, TypeName = "線板材"}
        //};

        /// <summary>
        /// 狀態表
        /// 1：入庫
        /// 2：出庫
        /// 3：送檢
        /// 4：送修
        /// </summary>
        /// <returns></returns>
        public static List<SpareType> GetStatus()
        {
            var res = new List<SpareType>();

            foreach (StatusEnum tValue in System.Enum.GetValues(typeof(StatusEnum)))
            {
                FieldInfo fieldInfo = tValue.GetType().GetField(tValue.ToString());
                DescriptionAttribute[] attributes =
                    (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

                res.Add(new SpareType
                {
                    TypeId = (int)tValue,
                    TypeName = attributes[0].Description
                });
            }

            return res;
        }
    }
}
