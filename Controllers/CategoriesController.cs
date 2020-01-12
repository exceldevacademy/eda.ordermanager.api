using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using eda.ordermanager.api.Data.Entities;
using eda.ordermanager.api.Data.Models.Category;
using eda.ordermanager.api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eda.ordermanager.api.Controllers
{
    [ApiController]
    [Route("api/v1/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository ??
                throw new ArgumentNullException(nameof(categoryRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet()]
        public ActionResult<IEnumerable<CategoryDto>> GetCategories()
        {
            var categoriesFromRepo = _categoryRepository.GetCategories();

            var categoriesDto = _mapper.Map<IEnumerable<CategoryDto>>(categoriesFromRepo);
            return Ok(categoriesDto);
        }

        [HttpGet("{categoryId}", Name ="GetCategory")]
        public ActionResult<CategoryDto> GetCategory(int categoryId)
        {
            var categoryFromRepo = _categoryRepository.GetCategory(categoryId);

            if (categoryFromRepo == null)
            {
                return NotFound();
            }

            var categoryDto = _mapper.Map<CategoryDto>(categoryFromRepo);

            return Ok(categoryDto);
        }

        [HttpPost]
        public ActionResult<CategoryDto> AddCategory(CategoryForCreationDto categoryForCreation)
        {
            var category = _mapper.Map<Category>(categoryForCreation);

            _categoryRepository.AddCategory(category);
            _categoryRepository.Save();

            var categoryDto = _mapper.Map<CategoryDto>(category);
            return CreatedAtRoute("GetCategory",
                new { categoryDto.CategoryId },
                categoryDto);
        }

        [HttpDelete("{categoryId}")]
        public ActionResult DeleteCategory(int categoryId)
        {
            var categoryFromRepo = _categoryRepository.GetCategory(categoryId);

            if (categoryFromRepo == null)
            {
                return NotFound();
            }

            _categoryRepository.DeleteCategory(categoryFromRepo);
            _categoryRepository.Save();

            return NoContent();
        }
    }
}