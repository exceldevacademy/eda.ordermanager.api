using eda.ordermanager.api.Data.Models.OrderItem;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eda.ordermanager.api.Data.Validators.OrderItemValidators
{
    public class OrderItemForUpdateValidator : OrderItemForManipulationValidator<OrderItemForUpdateDto>
    {
        public OrderItemForUpdateValidator()
        {
        }
    }
}
