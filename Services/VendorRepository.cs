using eda.ordermanager.api.Context;
using eda.ordermanager.api.Data.Entities;
using eda.ordermanager.api.Data.Models.Vendor;
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
    public class VendorRepository : IVendorRepository
    {
        private readonly OrdersManagerDbContext _context;
        private readonly SieveProcessor _sieveProcessor;

        public VendorRepository(OrdersManagerDbContext context, SieveProcessor sieveProcessor)
        {
            _context = context;
            _sieveProcessor = sieveProcessor;
        }

        public Vendor GetVendor(int vendorId)
        {
            return _context.Vendors.FirstOrDefault(v => v.VendorId == vendorId);
        }

        public PagedList<Vendor> GetVendors(VendorParametersDto vendorParameters)
        {
            if (vendorParameters == null)
            {
                throw new ArgumentNullException(nameof(vendorParameters));
            }

            var collection = _context.Vendors as IQueryable<Vendor>;

            if (!string.IsNullOrWhiteSpace(vendorParameters.VendorName))
            {
                var vendorName = vendorParameters.VendorName.Trim();
                collection = collection.Where(v => v.VendorName == vendorName);
            }

            var sieveModel = new SieveModel
            {
                Sorts = vendorParameters.SortOrder
            };

            collection = _sieveProcessor.Apply(sieveModel, collection);


            return PagedList<Vendor>.Create(collection,
                vendorParameters.PageNumber, 
                vendorParameters.PageSize);
        }

        public void AddVendor(Vendor vendor)
        {
            if(vendor == null)
            {
                throw new ArgumentNullException(nameof(vendor));
            }

            _context.Vendors.Add(vendor);
        }

        public void DeleteVendor(Vendor vendor)
        {
            if(vendor == null)
            {
                throw new ArgumentNullException(nameof(vendor));
            }

            _context.Vendors.Remove(vendor);
        }

        public void UpdateVendor(Vendor vendor)
        {
            // no implementation for now
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
