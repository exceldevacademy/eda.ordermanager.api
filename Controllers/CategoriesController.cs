using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using eda.ordermanager.api.Data.Entities;
using eda.ordermanager.api.Data.Models.Category;
using eda.ordermanager.api.Services.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
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
        public ActionResult<IEnumerable<CategoryDto>> GetCategories([FromQuery] CategoryParametersDto categoryParametersDto)
        {
            var categoriesFromRepo = _categoryRepository.GetCategories(categoryParametersDto);

            var paginationMetadata = new
            {
                totalCount = categoriesFromRepo.TotalCount,
                pageSize = categoriesFromRepo.PageSize,
                currentPage = categoriesFromRepo.CurrentPage,
                totalPages = categoriesFromRepo.TotalPages,
                previousPageLink = categoriesFromRepo.HasPrevious,
                nextPageLink = categoriesFromRepo.HasNext
            };

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata));

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

        [HttpPut("{categoryId}")]
        public IActionResult UpdateCategory(int categoryId, CategoryForUpdateDto category)
        {
            var categoryFromRepo = _categoryRepository.GetCategory(categoryId);

            if (categoryFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(category, categoryFromRepo);
            _categoryRepository.UpdateCategory(categoryFromRepo);

            _categoryRepository.Save();

            return NoContent();
        }

        [HttpPatch("{categoryId}")]
        public IActionResult PartiallyUpdateCategory(int categoryId, JsonPatchDocument<CategoryForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var existingCategory = _categoryRepository.GetCategory(categoryId);

            if (existingCategory == null)
            {
                return NotFound();
            }

            var categoryToPatch = _mapper.Map<CategoryForUpdateDto>(existingCategory); // map the category we got from the database to an updatable category model
            patchDoc.ApplyTo(categoryToPatch, ModelState); // apply patchdoc updates to the updatable category

            if (!TryValidateModel(categoryToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(categoryToPatch, existingCategory); // apply updates from the updatable category to the db entity so we can apply the updates to the database
            _categoryRepository.UpdateCategory(existingCategory); // apply business updates to data if needed

            _categoryRepository.Save(); // save changes in the database

            return NoContent();
        }
    }
}