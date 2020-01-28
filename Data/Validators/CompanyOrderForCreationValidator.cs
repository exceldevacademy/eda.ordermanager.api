using eda.ordermanager.api.Data.Models.CompanyOrder;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eda.ordermanager.api.Data.Validators
{
    public class CompanyOrderForCreationValidator : CompanyOrderForManipulationValidator<CompanyOrderForCreationDto>
    {
        public CompanyOrderForCreationValidator()
        {
        }
    }
}
