using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using eda.ordermanager.api.Data.Entities;
using eda.ordermanager.api.Data.Models.CompanyOrder;
using eda.ordermanager.api.Data.Models.Vendor;
using eda.ordermanager.api.Services.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace eda.ordermanager.api.Controllers
{
    [ApiController]
    [Route("api/v1/companyOrders")]
    public class CompanyOrdersController : ControllerBase
    {
        private readonly ICompanyOrderRepository _companyOrderRepository;
        private readonly IMapper _mapper;

        public CompanyOrdersController(ICompanyOrderRepository companyOrderRepository, IMapper mapper)
        {
            _companyOrderRepository = companyOrderRepository ??
                throw new ArgumentNullException(nameof(companyOrderRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet()]
        public ActionResult<IEnumerable<CompanyOrderDto>> GetCompanyOrders([FromQuery] CompanyOrderParametersDto companyOrderParameters)
        {
            var companyOrdersFromRepo = _companyOrderRepository.GetCompanyOrders(companyOrderParameters);

            var paginationMetadata = new
            {
                totalCount = companyOrdersFromRepo.TotalCount,
                pageSize = companyOrdersFromRepo.PageSize,
                currentPage = companyOrdersFromRepo.CurrentPage,
                totalPages = companyOrdersFromRepo.TotalPages,
                previousPageLink = companyOrdersFromRepo.HasPrevious,
                nextPageLink = companyOrdersFromRepo.HasNext
            };

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata));

            var companyOrdersDto = _mapper.Map<IEnumerable<CompanyOrderDto>>(companyOrdersFromRepo);
            return Ok(companyOrdersDto);
        }

        [HttpGet("{companyOrderId}", Name ="GetCompanyOrder")]
        public ActionResult<CompanyOrderDto> GetCompanyOrder(int companyOrderId)
        {
            var companyOrderFromRepo = _companyOrderRepository.GetCompanyOrder(companyOrderId);

            if (companyOrderFromRepo == null)
            {
                return NotFound();
            }

            var companyOrderDto = _mapper.Map<CompanyOrderDto>(companyOrderFromRepo);

            return Ok(companyOrderDto);
        }

        [HttpPost]
        public ActionResult<CompanyOrderDto> AddCompanyOrder(CompanyOrderForCreationDto companyOrderForCreation)
        {
            var companyOrder = _mapper.Map<CompanyOrder>(companyOrderForCreation);

            _companyOrderRepository.AddCompanyOrder(companyOrder);
            _companyOrderRepository.Save();

            var companyOrderDto = _mapper.Map<CompanyOrderDto>(companyOrder);
            return CreatedAtRoute("GetCompanyOrder",
                new { companyOrderDto.CompanyOrderId },
                companyOrderDto);
        }

        [HttpDelete("{companyOrderId}")]
        public ActionResult DeleteCompanyOrder(int companyOrderId)
        {
            var companyOrderFromRepo = _companyOrderRepository.GetCompanyOrder(companyOrderId);

            if (companyOrderFromRepo == null)
            {
                return NotFound();
            }

            _companyOrderRepository.DeleteCompanyOrder(companyOrderFromRepo);
            _companyOrderRepository.Save();

            return NoContent();
        }

        [HttpPut("{companyOrderId}")]
        public IActionResult UpdateCompanyOrder(int companyOrderId, CompanyOrderForUpdateDto companyOrder)
        {
            var companyOrderFromRepo = _companyOrderRepository.GetCompanyOrder(companyOrderId);

            if (companyOrderFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(companyOrder, companyOrderFromRepo);
            _companyOrderRepository.UpdateCompanyOrder(companyOrderFromRepo);

            _companyOrderRepository.Save();

            return NoContent();
        }

        [HttpPatch("{companyOrderId}")]
        public IActionResult PartiallyUpdateCompanyOrder(int companyOrderId, JsonPatchDocument<CompanyOrderForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var existingCompanyOrder = _companyOrderRepository.GetCompanyOrder(companyOrderId);

            if (existingCompanyOrder == null)
            {
                return NotFound();
            }

            var companyOrderToPatch = _mapper.Map<CompanyOrderForUpdateDto>(existingCompanyOrder); // map the companyOrder we got from the database to an updatable companyOrder model
            patchDoc.ApplyTo(companyOrderToPatch, ModelState); // apply patchdoc updates to the updatable companyOrder

            if (!TryValidateModel(companyOrderToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(companyOrderToPatch, existingCompanyOrder); // apply updates from the updatable companyOrder to the db entity so we can apply the updates to the database
            _companyOrderRepository.UpdateCompanyOrder(existingCompanyOrder); // apply business updates to data if needed

            _companyOrderRepository.Save(); // save changes in the database

            return NoContent();
        }
    }
}