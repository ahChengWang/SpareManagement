using SpareManagement.Repository.Dao;
using System.Collections.Generic;

namespace SpareManagement.Repository
{
    public interface ICategoryRepository
    {
        List<CategoryDao> SelectByConditions(int categoryId);
    }
}