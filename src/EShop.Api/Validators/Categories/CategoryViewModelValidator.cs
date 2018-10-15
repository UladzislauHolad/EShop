using EShop.Api.Models.CategoriesViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Api.Validators.Categories
{
    public class CategoryViewModelValidator : AbstractValidator<CategoryViewModel>
    {
        public CategoryViewModelValidator()
        {
            RuleFor(c => c.Name).NotNull();
            RuleFor(c => c.Name).MinimumLength(2);
            RuleFor(c => c.Name).MaximumLength(50);

            RuleFor(c => c.ParentId).NotNull();
        }
    }
}
