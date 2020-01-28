using eda.ordermanager.api.Data.Models.OrderItem;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eda.ordermanager.api.Data.Validators.OrderItemValidators
{
    public class OrderItemForManipulationValidator<T> : AbstractValidator<T> where T : OrderItemForManipulationDto
    {
        public OrderItemForManipulationValidator()
        {
            RuleFor(oi => oi.Status).NotEmpty();
            RuleFor(oi => oi.ProductName).NotEmpty();
            RuleFor(oi => oi.Amount).GreaterThanOrEqualTo(0);
            RuleFor(oi => oi.Amount).NotEmpty();
        }
    }
}
