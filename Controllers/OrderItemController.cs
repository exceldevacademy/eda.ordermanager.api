using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using eda.ordermanager.api.Data.Entities;
using eda.ordermanager.api.Data.Models.OrderItem;
using eda.ordermanager.api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eda.ordermanager.api.Controllers
{
    [ApiController]
    [Route("api/v1/orderItems")]
    public class OrderItemsController : ControllerBase
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IMapper _mapper;

        public OrderItemsController(IOrderItemRepository orderItemRepository, IMapper mapper)
        {
            _orderItemRepository = orderItemRepository ??
                throw new ArgumentNullException(nameof(orderItemRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet()]
        public ActionResult<IEnumerable<OrderItemDto>> GetOrderItems()
        {
            var orderItemsFromRepo = _orderItemRepository.GetOrderItems();

            var orderItemsDto = _mapper.Map<IEnumerable<OrderItemDto>>(orderItemsFromRepo);
            return Ok(orderItemsDto);
        }

        [HttpGet("{orderItemId}", Name ="GetOrderItem")]
        public ActionResult<OrderItemDto> GetOrderItem(int orderItemId)
        {
            var orderItemFromRepo = _orderItemRepository.GetOrderItem(orderItemId);

            if (orderItemFromRepo == null)
            {
                return NotFound();
            }

            var orderItemDto = _mapper.Map<OrderItemDto>(orderItemFromRepo);
            return Ok(orderItemDto);
        }

        [HttpPost]
        public ActionResult<OrderItemDto> AddOrderItem(OrderItemForCreationDto orderItemForCreation)
        {
            var orderItem = _mapper.Map<OrderItem>(orderItemForCreation);
            _orderItemRepository.AddOrderItem(orderItem);
            _orderItemRepository.Save();

            var orderItemDto = _mapper.Map<OrderItemDto>(orderItem);
            return CreatedAtRoute("GetOrderItem",
                new { orderItemDto.OrderItemId },
                orderItemDto);
        }

        [HttpDelete("{orderItemId}")]
        public ActionResult DeleteOrderItem(int orderItemId)
        {
            var orderItemFromRepo = _orderItemRepository.GetOrderItem(orderItemId);

            if (orderItemFromRepo == null)
            {
                return NotFound();
            }

            _orderItemRepository.DeleteOrderItem(orderItemFromRepo);
            _orderItemRepository.Save();

            return NoContent();
        }
    }
}