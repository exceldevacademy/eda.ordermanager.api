using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eda.ordermanager.api.Data.Entities;
using eda.ordermanager.api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eda.ordermanager.api.Controllers
{
    [ApiController]
    [Route("api/v1/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository ??
                throw new ArgumentNullException(nameof(categoryRepository));
        }

        [HttpGet()]
        public ActionResult<IEnumerable<Category>> GetCategories()
        {
            var categorysFromRepo = _categoryRepository.GetCategories();
            return Ok(categorysFromRepo);
        }

        [HttpGet("{categoryId}", Name ="GetCategory")]
        public ActionResult<Category> GetCategory(int categoryId)
        {
            var categoryFromRepo = _categoryRepository.GetCategory(categoryId);

            if (categoryFromRepo == null)
            {
                return NotFound();
            }

            return Ok(categoryFromRepo);
        }

        [HttpPost]
        public ActionResult<Category> AddCategory(Category category)
        {
            _categoryRepository.AddCategory(category);
            _categoryRepository.Save();

            return CreatedAtRoute("GetCategory",
                new { category.CategoryId },
                category);
        }
    }
}