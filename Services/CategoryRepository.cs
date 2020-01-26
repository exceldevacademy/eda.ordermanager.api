using eda.ordermanager.api.Context;
using eda.ordermanager.api.Data.Entities;
using eda.ordermanager.api.Data.Models.Category;
using eda.ordermanager.api.Helpers;
using eda.ordermanager.api.Services.Interfaces;
using Sieve.Models;
using Sieve.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eda.ordermanager.api.Services
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly OrdersManagerDbContext _context;
        private readonly SieveProcessor _sieveProcessor;

        public CategoryRepository(OrdersManagerDbContext context, SieveProcessor sieveProcessor)
        {
            _context = context;
            _sieveProcessor = sieveProcessor;
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

            if (!string.IsNullOrWhiteSpace(categoryParameters.CategoryName))
            {
                var categoryName = categoryParameters.CategoryName.Trim();
                collection = collection.Where(c => c.CategoryName == categoryName);
            }

            var sieveModel = new SieveModel
            {
                Sorts = categoryParameters.SortOrder
            };

            collection = _sieveProcessor.Apply(sieveModel, collection);


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
