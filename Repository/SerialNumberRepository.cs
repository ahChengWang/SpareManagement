using Helper;
using SpareManagement.Repository.Dao;
using System.Collections.Generic;
using System.Linq;

namespace SpareManagement.Repository
{
    public class SerialNumberRepository : BaseRepository
    {

        public SerialNumberDao GetNumber(int categoryId)
        {
            var sql = $"select TOP 1 * from Serial_Number where CategoryId=@categoryId ";
            
            var _result = _dbHelper.ExecuteQuery<SerialNumberDao>(sql, new {
                categoryId = categoryId
            }).FirstOrDefault();

            return _result;
        }


        public int UpdateNumber(int categoryId, int increaseCnt, bool isForWarehouse)
        {
            var sql = $"update Serial_Number set ";

            if (isForWarehouse)
            {
                sql += $" LastSerialNo = LastSerialNo + @increaseCnt ";
            }
            else
            {
                sql += $" LastMaterialNo = LastMaterialNo + 1 ";                
            }
            sql += "where CategoryId = @CategoryId; ";

            var _result = _dbHelper.ExecuteNonQuery(sql, new
            {
                categoryId = categoryId,
                increaseCnt = increaseCnt
            });

            return _result;
        }
    }
}
