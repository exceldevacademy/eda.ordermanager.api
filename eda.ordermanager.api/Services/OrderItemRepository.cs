using eda.ordermanager.api.Context;
using eda.ordermanager.api.Data.Entities;
using eda.ordermanager.api.Data.Models.OrderItem;
using eda.ordermanager.api.Helpers;
using eda.ordermanager.api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eda.ordermanager.api.Services
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly OrdersManagerDbContext _context;
        private readonly SieveProcessor _sieveProcessor;

        public OrderItemRepository(OrdersManagerDbContext context, SieveProcessor sieveProcessor)
        {
            _context = context;
            _sieveProcessor = sieveProcessor;
        }

        public OrderItem GetOrderItem(int orderItemId)
        {
            return _context.OrderItems
              .Include(oi => oi.Order)
              .Include(oi => oi.Category)
              .FirstOrDefault(v => v.OrderItemId == orderItemId);
        }

        public PagedList<OrderItem> GetOrderItems(OrderItemParametersDto orderItemParameters)
        {

            if (orderItemParameters == null)
            {
                throw new ArgumentNullException(nameof(orderItemParameters));
            }

            var collection = _context.OrderItems
              .Include(oi => oi.Order)
              .Include(oi => oi.Category) as IQueryable<OrderItem>;

            if (!string.IsNullOrWhiteSpace(orderItemParameters.ExternalOrderNo))
            {
                var externalOrderNo = orderItemParameters.ExternalOrderNo.Trim();
                collection = collection.Where(oi => oi.Order.ExternalOrderNo == externalOrderNo);
            }

            if (!string.IsNullOrWhiteSpace(orderItemParameters.QueryString))
            {
                var QueryString = orderItemParameters.QueryString.Trim();
                collection = collection.Where(oi => oi.ProductName.Contains(QueryString)
                    || oi.Status.Contains(QueryString)
                    || oi.Comments.Contains(QueryString));
            }

            var sieveModel = new SieveModel
            {
                Sorts = orderItemParameters.SortOrder
            };

            collection = _sieveProcessor.Apply(sieveModel, collection);


            return PagedList<OrderItem>.Create(collection,
                orderItemParameters.PageNumber,
                orderItemParameters.PageSize);
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

        public void UpdateOrderItem(OrderItem orderItem)
        {
            // no implementation for now
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
