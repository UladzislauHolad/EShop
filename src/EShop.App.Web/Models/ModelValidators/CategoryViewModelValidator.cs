using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.App.Web.Models.ModelValidators
{
    public class CategoryViewModelValidator : AbstractValidator<CategoryViewModel>
    {
        public CategoryViewModelValidator()
        {
            RuleFor(c => c.CategoryId).GreaterThan(-1);
            RuleFor(c => c.Name).NotEmpty().MaximumLength(50);
            RuleFor(c => c.ParentId).GreaterThan(-1);
        }
    }
}
