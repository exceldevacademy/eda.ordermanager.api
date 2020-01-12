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
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly OrdersManagerDbContext _context;
        public OrderItemRepository(OrdersManagerDbContext context)
        {
            _context = context;
        }

        public OrderItem GetOrderItem(int orderItemId)
        {
            return _context.OrderItems
              .Include(oi => oi.Order)
              .Include(oi => oi.Category)
              .FirstOrDefault(v => v.OrderItemId == orderItemId);
        }

        public IEnumerable<OrderItem> GetOrderItems()
        {
            return _context.OrderItems
              .Include(oi => oi.Order)
              .Include(oi => oi.Category)
              .ToList<OrderItem>();
        }

        public void AddOrderItem(OrderItem orderItem)
        {
            if(orderItem == null)
            {
                throw new ArgumentNullException(nameof(orderItem));
            }

            _context.OrderItems.Add(orderItem);
        }

        public void DeleteOrderItem(OrderItem orderItem)
        {
            if (orderItem == null)
            {
                throw new ArgumentNullException(nameof(orderItem));
            }

            _context.OrderItems.Remove(orderItem);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
