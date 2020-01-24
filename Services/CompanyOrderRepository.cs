using eda.ordermanager.api.Context;
using eda.ordermanager.api.Data.Entities;
using eda.ordermanager.api.Data.Models.CompanyOrder;
using eda.ordermanager.api.Helpers;
using eda.ordermanager.api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eda.ordermanager.api.Services
{
    public class CompanyOrderRepository : ICompanyOrderRepository
    {
        private readonly OrdersManagerDbContext _context;
        public CompanyOrderRepository(OrdersManagerDbContext context)
        {
            _context = context;
        }

        public CompanyOrder GetCompanyOrder(int orderid)
        {
            return _context.CompanyOrders
              .Include(oi => oi.Vendor)
              .FirstOrDefault(co => co.CompanyOrderId == orderid);
        }

        public PagedList<CompanyOrder> GetCompanyOrders(CompanyOrderParametersDto companyOrderParameters)
        {
            if (companyOrderParameters == null)
            {
                throw new ArgumentNullException(nameof(companyOrderParameters));
            }

            var collection = _context.CompanyOrders
              .Include(co => co.Vendor) as IQueryable<CompanyOrder>;

            return PagedList<CompanyOrder>.Create(collection,
                companyOrderParameters.PageNumber,
                companyOrderParameters.PageSize);
        }

        public void AddCompanyOrder(CompanyOrder companyOrder)
        {
            if(companyOrder == null)
            {
                throw new ArgumentNullException(nameof(companyOrder));
            }

            _context.CompanyOrders.Add(companyOrder);
        }

        public void DeleteCompanyOrder(CompanyOrder companyOrder)
        {
            if (companyOrder == null)
            {
                throw new ArgumentNullException(nameof(companyOrder));
            }

            _context.CompanyOrders.Remove(companyOrder);
        }

        public void UpdateCompanyOrder(CompanyOrder companyOrder)
        {
            // no implementation for now
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
