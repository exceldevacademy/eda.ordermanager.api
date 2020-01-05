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
    [Route("api/v1/companyOrders")]
    public class CompanyOrdersController : ControllerBase
    {
        private readonly ICompanyOrderRepository _companyOrderRepository;

        public CompanyOrdersController(ICompanyOrderRepository companyOrderRepository)
        {
            _companyOrderRepository = companyOrderRepository ??
                throw new ArgumentNullException(nameof(companyOrderRepository));
        }

        [HttpGet()]
        public ActionResult<IEnumerable<CompanyOrder>> GetCompanyOrders()
        {
            var companyOrdersFromRepo = _companyOrderRepository.GetCompanyOrders();
            return Ok(companyOrdersFromRepo);
        }

        [HttpGet("{companyOrderId}", Name ="GetCompanyOrder")]
        public ActionResult<CompanyOrder> GetCompanyOrder(int companyOrderId)
        {
            var companyOrderFromRepo = _companyOrderRepository.GetCompanyOrder(companyOrderId);

            if (companyOrderFromRepo == null)
            {
                return NotFound();
            }

            return Ok(companyOrderFromRepo);
        }

        [HttpPost]
        public ActionResult<CompanyOrder> AddCompanyOrder(CompanyOrder companyOrder)
        {
            _companyOrderRepository.AddCompanyOrder(companyOrder);
            _companyOrderRepository.Save();

            return CreatedAtRoute("GetCompanyOrder",
                new { companyOrder.CompanyOrderId },
                companyOrder);
        }
    }
}