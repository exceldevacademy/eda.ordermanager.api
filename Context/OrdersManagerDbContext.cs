using eda.ordermanager.api.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eda.ordermanager.api.Context
{
    public class OrdersManagerDbContext : DbContext
    {
        public OrdersManagerDbContext(DbContextOptions<OrdersManagerDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<CompanyOrder> CompanyOrders { get; set; }
    }
}
