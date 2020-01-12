using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using eda.ordermanager.api.Data.Entities;
using eda.ordermanager.api.Data.Models.CompanyOrder;
using eda.ordermanager.api.Data.Models.Vendor;
using eda.ordermanager.api.Services.Interfaces;
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
        public ActionResult<IEnumerable<CompanyOrderDto>> GetCompanyOrders()
        {
            var companyOrdersFromRepo = _companyOrderRepository.GetCompanyOrders();

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
    }
}