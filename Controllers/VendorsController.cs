using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public VendorsController(IVendorRepository vendorRepository, IMapper mapper)
        {
            _vendorRepository = vendorRepository ??
                throw new ArgumentNullException(nameof(vendorRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet()]
        public ActionResult<IEnumerable<VendorDto>> GetVendors()
        {
            var vendorsFromRepo = _vendorRepository.GetVendors();

            var vendorsDto = _mapper.Map<IEnumerable<VendorDto>>(vendorsFromRepo);
            return Ok(vendorsDto);
        }

        [HttpGet("{vendorId}", Name ="GetVendor")]
        public ActionResult<VendorDto> GetVendor(int vendorId)
        {
            var vendorFromRepo = _vendorRepository.GetVendor(vendorId);

            if (vendorFromRepo == null)
            {
                return NotFound();
            }

            var vendorDto = _mapper.Map<VendorDto>(vendorFromRepo);

            return Ok(vendorDto);
        }

        [HttpPost]
        public ActionResult<VendorDto> AddVendor(VendorForCreationDto vendorForCreation)
        {
            var vendor = _mapper.Map<Vendor>(vendorForCreation);
            _vendorRepository.AddVendor(vendor);
            _vendorRepository.Save();


            var vendorDto = _mapper.Map<VendorDto>(vendor);
            return CreatedAtRoute("GetVendor",
                new { vendorDto.VendorId },
                vendorDto);
        }

        [HttpDelete("{vendorId}")]
        public ActionResult DeleteVendor(int vendorId)
        {
            var vendorFromRepo = _vendorRepository.GetVendor(vendorId);

            if(vendorFromRepo == null)
            {
                return NotFound();
            }

            _vendorRepository.DeleteVendor(vendorFromRepo);
            _vendorRepository.Save();

            return NoContent();
        }

        [HttpPut("{vendorId}")]
        public IActionResult UpdateVendor(int vendorId, VendorForUpdateDto vendor)
        {
            var vendorFromRepo = _vendorRepository.GetVendor(vendorId);

            if (vendorFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(vendor, vendorFromRepo);
            _vendorRepository.UpdateVendor(vendorFromRepo);

            _vendorRepository.Save();

            return NoContent();
        }
    }
}