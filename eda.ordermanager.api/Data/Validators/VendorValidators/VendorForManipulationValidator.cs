using eda.ordermanager.api.Data.Models.Vendor;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eda.ordermanager.api.Data.Validators.VendorValidators
{
    public class VendorForManipulationValidator<T> : AbstractValidator<T> where T : VendorForManipulationDto
    {
        public VendorForManipulationValidator()
        {
            RuleFor(v => v.VendorName).NotEmpty();
        }
    }
}
