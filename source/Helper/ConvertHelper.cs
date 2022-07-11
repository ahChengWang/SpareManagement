using System.Collections.Generic;
using System.Linq;

namespace SpareManagement.Helper
{
    public static class ConvertHelper
    {
        public static List<T> CopyAToB<T>(this IEnumerable<object> R) where T : new()
        {
            if (R == null || !R.Any())
                return new List<T> { };

            // copy fields
            var typeOfA = R.First().GetType();
            var targetBList = new List<T>();
            foreach (var item in R)
            {
                var targetB = new T();
                foreach (var fieldOfA in typeOfA.GetProperties())
                {
                    var fieldOfB = targetB.GetType().GetProperty(fieldOfA.Name);
                    if (fieldOfB == null)
                        continue;
                    fieldOfB.SetValue(targetB, typeOfA.GetProperty(fieldOfA.Name).GetValue(item));
                }
                targetBList.Add(targetB);
            }

            return targetBList;
        }
    }
}
