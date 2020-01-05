using eda.ordermanager.api.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eda.ordermanager.api.Services.Interfaces
{
    public interface IOrderItemRepository
    {
        OrderItem GetOrderItem(int orderItemId);
        IEnumerable<OrderItem> GetOrderItems();
        void AddOrderItem(OrderItem orderItem);
        bool Save();
    }
}
