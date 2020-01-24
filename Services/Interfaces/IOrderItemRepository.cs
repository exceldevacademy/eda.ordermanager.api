using eda.ordermanager.api.Data.Entities;
using eda.ordermanager.api.Data.Models.OrderItem;
using eda.ordermanager.api.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eda.ordermanager.api.Services.Interfaces
{
    public interface IOrderItemRepository
    {
        OrderItem GetOrderItem(int orderItemId);
        PagedList<OrderItem> GetOrderItems(OrderItemParametersDto orderItemParameters);
        void AddOrderItem(OrderItem orderItem);
        void DeleteOrderItem(OrderItem orderItem);
        void UpdateOrderItem(OrderItem orderItem);
        bool Save();
    }
}
