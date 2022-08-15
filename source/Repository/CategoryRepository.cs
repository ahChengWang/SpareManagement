using Helper;
using SpareManagement.Repository.Dao;
using System;
using System.Collections.Generic;

namespace SpareManagement.Repository
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {

        public List<CategoryDao> SelectByConditions(int categoryId)
        {
            try
            {
                var sql = @"select * from Category where Id = @CategoryId; ";

                var _result = _dbHelper.ExecuteQuery<CategoryDao>(sql, new
                {
                    CategoryId = categoryId
                });

                return _result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
