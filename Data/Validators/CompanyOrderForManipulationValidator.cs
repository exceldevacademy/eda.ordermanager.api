using eda.ordermanager.api.Data.Models.CompanyOrder;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eda.ordermanager.api.Data.Validators
{
    public class CompanyOrderForManipulationValidator<T> : AbstractValidator<T> where T : CompanyOrderForManipulationDto
    {
        public CompanyOrderForManipulationValidator()
        {
            RuleFor(co => co.Amount).GreaterThanOrEqualTo(0);
            RuleFor(co => co.Amount).NotEmpty();
            RuleFor(co => co.ArrivalDate).LessThanOrEqualTo(DateTime.UtcNow).WithMessage("'Arrival Date' can not be set in the future.");
            RuleFor(co => co.PurchaseDate).LessThanOrEqualTo(DateTime.UtcNow).WithMessage("'Purchase Date' can not be set in the future.");
            RuleFor(co => co.PurchaseDate).LessThanOrEqualTo(co => co.ArrivalDate).WithMessage("'Purchase Date' must be less than or equal to 'Arrival Date'.");
        }
    }
}
