using eda.ordermanager.api.Data.Entities;
using eda.ordermanager.api.Data.Models.CompanyOrder;
using eda.ordermanager.api.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eda.ordermanager.api.Services.Interfaces
{
    public interface ICompanyOrderRepository
    {
        CompanyOrder GetCompanyOrder(int companyOrderId);
        PagedList<CompanyOrder> GetCompanyOrders(CompanyOrderParametersDto companyOrderParameters);
        void AddCompanyOrder(CompanyOrder companyOrder);
        void DeleteCompanyOrder(CompanyOrder companyOrder);
        void UpdateCompanyOrder(CompanyOrder companyOrder);
        bool Save();
    }
}
