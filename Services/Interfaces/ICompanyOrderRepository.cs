using eda.ordermanager.api.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eda.ordermanager.api.Services.Interfaces
{
    public interface ICompanyOrderRepository
    {
        CompanyOrder GetCompanyOrder(int companyOrderId);
        IEnumerable<CompanyOrder> GetCompanyOrders();
        void AddCompanyOrder(CompanyOrder companyOrder);
        void DeleteCompanyOrder(CompanyOrder companyOrder);
        bool Save();
    }
}
