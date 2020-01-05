using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eda.ordermanager.api.Data.Entities;
using eda.ordermanager.api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eda.ordermanager.api.Controllers
{
    [ApiController]
    [Route("api/v1/orderItems")]
    public class OrderItemsController : ControllerBase
    {
        private readonly IOrderItemRepository _orderItemRepository;

        public OrderItemsController(IOrderItemRepository orderItemRepository)
        {
            _orderItemRepository = orderItemRepository ??
                throw new ArgumentNullException(nameof(orderItemRepository));
        }

        [HttpGet()]
        public ActionResult<IEnumerable<OrderItem>> GetOrderItems()
        {
            var orderItemsFromRepo = _orderItemRepository.GetOrderItems();
            return Ok(orderItemsFromRepo);
        }

        [HttpGet("{orderItemId}", Name ="GetOrderItem")]
        public ActionResult<OrderItem> GetOrderItem(int orderItemId)
        {
            var orderItemFromRepo = _orderItemRepository.GetOrderItem(orderItemId);

            if (orderItemFromRepo == null)
            {
                return NotFound();
            }

            return Ok(orderItemFromRepo);
        }

        [HttpPost]
        public ActionResult<OrderItem> AddOrderItem(OrderItem orderItem)
        {
            _orderItemRepository.AddOrderItem(orderItem);
            _orderItemRepository.Save();

            return CreatedAtRoute("GetOrderItem",
                new { orderItem.OrderItemId },
                orderItem);
        }
    }
}