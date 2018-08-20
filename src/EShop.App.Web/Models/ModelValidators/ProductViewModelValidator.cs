﻿using FluentValidation;

namespace EShop.App.Web.Models.ModelValidators
{
    public class ProductViewModelValidator : AbstractValidator<ProductViewModel>
    {
        public ProductViewModelValidator()
        {
            RuleFor(prod => prod.Name).NotEmpty().MaximumLength(100);
            RuleFor(prod => prod.Price).GreaterThan(0);
            RuleFor(prod => prod.Description).NotEmpty().MaximumLength(500);
            RuleFor(prod => prod.CategoriesId).NotNull();
            RuleFor(prod => prod.Count).GreaterThan(-1);
        }
    }
}
