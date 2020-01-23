using eda.ordermanager.api.Data.Entities;
using eda.ordermanager.api.Data.Models.Vendor;
using eda.ordermanager.api.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eda.ordermanager.api.Services.Interfaces
{
    public interface IVendorRepository
    {
        Vendor GetVendor(int vendorId);
        PagedList<Vendor> GetVendors(VendorParametersDto vendorParameters);
        void AddVendor(Vendor vendor);
        void DeleteVendor(Vendor vendor);
        void UpdateVendor(Vendor vendor);
        bool Save();
    }
}
