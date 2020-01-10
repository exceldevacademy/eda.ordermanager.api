using eda.ordermanager.api.Context;
using eda.ordermanager.api.Data.Entities;
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

        public IEnumerable<CompanyOrder> GetCompanyOrders()
        {
            return _context.CompanyOrders
              .Include(oi => oi.Vendor)
              .ToList<CompanyOrder>();
        }

        public void AddCompanyOrder(CompanyOrder companyOrder)
        {
            if(companyOrder == null)
            {
                throw new ArgumentNullException(nameof(companyOrder));
            }

            _context.CompanyOrders.Add(companyOrder);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
