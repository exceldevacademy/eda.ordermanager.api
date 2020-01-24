using eda.ordermanager.api.Context;
using eda.ordermanager.api.Data.Entities;
using eda.ordermanager.api.Data.Models.Category;
using eda.ordermanager.api.Helpers;
using eda.ordermanager.api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eda.ordermanager.api.Services
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly OrdersManagerDbContext _context;
        public CategoryRepository(OrdersManagerDbContext context)
        {
            _context = context;
        }

        public Category GetCategory(int categoryId)
        {
            return _context.Categories.FirstOrDefault(c => c.CategoryId == categoryId);
        }

        public PagedList<Category> GetCategories(CategoryParametersDto categoryParameters)
        {

            if (categoryParameters == null)
            {
                throw new ArgumentNullException(nameof(categoryParameters));
            }

            var collection = _context.Categories as IQueryable<Category>;

            return PagedList<Category>.Create(collection,
                categoryParameters.PageNumber,
                categoryParameters.PageSize);
        }

        public void AddCategory(Category category)
        {
            if(category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            _context.Categories.Add(category);
        }

        public void DeleteCategory(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            _context.Categories.Remove(category);
        }

        public void UpdateCategory(Category category)
        {
            // no implementation for now
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
