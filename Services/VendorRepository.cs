using eda.ordermanager.api.Context;
using eda.ordermanager.api.Data.Entities;
using eda.ordermanager.api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eda.ordermanager.api.Services
{
    public class VendorRepository : IVendorRepository
    {
        private readonly OrdersManagerDbContext _context;
        public VendorRepository(OrdersManagerDbContext context)
        {
            _context = context;
        }

        public Vendor GetVendor(int vendorId)
        {
            return _context.Vendors.FirstOrDefault(v => v.VendorId == vendorId);
        }

        public IEnumerable<Vendor> GetVendors()
        {
            return _context.Vendors.ToList<Vendor>();
        }

        public void AddVendor(Vendor vendor)
        {
            if(vendor == null)
            {
                throw new ArgumentNullException(nameof(vendor));
            }

            _context.Vendors.Add(vendor);
        }
    }
}
