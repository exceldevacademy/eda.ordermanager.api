using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eda.ordermanager.api.Data.Entities;
using eda.ordermanager.api.Data.Models.Vendor;
using eda.ordermanager.api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eda.ordermanager.api.Controllers
{
    [ApiController]
    [Route("api/v1/vendors")]
    public class VendorsController : ControllerBase
    {
        private readonly IVendorRepository _vendorRepository;

        public VendorsController(IVendorRepository vendorRepository)
        {
            _vendorRepository = vendorRepository ??
                throw new ArgumentNullException(nameof(vendorRepository));
        }

        [HttpGet()]
        public ActionResult<IEnumerable<Vendor>> GetVendors()
        {
            var vendorsFromRepo = _vendorRepository.GetVendors();
            return Ok(vendorsFromRepo);
        }

        [HttpGet("{vendorId}", Name ="GetVendor")]
        public ActionResult<VendorDto> GetVendor(int vendorId)
        {
            var vendorFromRepo = _vendorRepository.GetVendor(vendorId);

            if (vendorFromRepo == null)
            {
                return NotFound();
            }

            var vendorDto = new VendorDto
            {
                VendorId = vendorFromRepo.VendorId,
                VendorName = vendorFromRepo.VendorName
            };

            return Ok(vendorDto);
        }

        [HttpPost]
        public ActionResult<Vendor> AddVendor(Vendor vendor)
        {
            _vendorRepository.AddVendor(vendor);
            _vendorRepository.Save();

            return CreatedAtRoute("GetVendor",
                new { vendor.VendorId },
                vendor);
        }
    }
}