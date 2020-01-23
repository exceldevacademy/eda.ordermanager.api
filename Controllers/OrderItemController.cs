using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using eda.ordermanager.api.Data.Entities;
using eda.ordermanager.api.Data.Models.OrderItem;
using eda.ordermanager.api.Services.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
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

        [HttpPut("{orderItemId}")]
        public IActionResult UpdateOrderItem(int orderItemId, OrderItemForUpdateDto orderItem)
        {
            var orderItemFromRepo = _orderItemRepository.GetOrderItem(orderItemId);

            if (orderItemFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(orderItem, orderItemFromRepo);
            _orderItemRepository.UpdateOrderItem(orderItemFromRepo);

            _orderItemRepository.Save();

            return NoContent();
        }

        [HttpPatch("{orderItemId}")]
        public IActionResult PartiallyUpdateOrderItem(int orderItemId, JsonPatchDocument<OrderItemForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var existingOrderItem = _orderItemRepository.GetOrderItem(orderItemId);

            if (existingOrderItem == null)
            {
                return NotFound();
            }

            var orderItemToPatch = _mapper.Map<OrderItemForUpdateDto>(existingOrderItem); // map the orderItem we got from the database to an updatable orderItem model
            patchDoc.ApplyTo(orderItemToPatch, ModelState); // apply patchdoc updates to the updatable orderItem

            if (!TryValidateModel(orderItemToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(orderItemToPatch, existingOrderItem); // apply updates from the updatable orderItem to the db entity so we can apply the updates to the database
            _orderItemRepository.UpdateOrderItem(existingOrderItem); // apply business updates to data if needed

            _orderItemRepository.Save(); // save changes in the database

            return NoContent();
        }
    }
}