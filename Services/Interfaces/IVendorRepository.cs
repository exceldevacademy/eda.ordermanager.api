using eda.ordermanager.api.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eda.ordermanager.api.Services.Interfaces
{
    public interface IVendorRepository
    {
        Vendor GetVendor(int vendorId);
        IEnumerable<Vendor> GetVendors();
        void AddVendor(Vendor vendor);
        bool Save();
    }
}
