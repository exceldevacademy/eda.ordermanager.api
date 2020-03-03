using eda.ordermanager.api.Data.Models.Category;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eda.ordermanager.api.Data.Validators.CategoryValidators
{
    public class CategoryForUpdateValidator : CategoryForManipulationValidator<CategoryForUpdateDto>
    {
        public CategoryForUpdateValidator()
        {
        }
    }
}
