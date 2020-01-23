using eda.ordermanager.api.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eda.ordermanager.api.Services.Interfaces
{
    public interface ICategoryRepository
    {
        Category GetCategory(int categoryid);
        IEnumerable<Category> GetCategories();
        void AddCategory(Category category);
        void DeleteCategory(Category category);
        void UpdateCategory(Category category);
        bool Save();
    }
}
