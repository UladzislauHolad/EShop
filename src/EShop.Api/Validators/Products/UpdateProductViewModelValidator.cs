using EShop.Api.Models.ProductsViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Api.Validators.Products
{
    public class UpdateProductViewModelValidator : AbstractValidator<UpdateProductViewModel>
    {
        public UpdateProductViewModelValidator()
        {
            RuleFor(prod => prod.ProductId).NotNull();
            RuleFor(prod => prod.ProductId).GreaterThan(0);

            RuleFor(prod => prod.Name).NotNull();
            RuleFor(prod => prod.Name).MinimumLength(4);
            RuleFor(prod => prod.Name).MaximumLength(50);

            RuleFor(prod => prod.Price).NotNull();
            RuleFor(prod => prod.Price).GreaterThan(0.01m);

            RuleFor(prod => prod.Count).NotNull();
            RuleFor(prod => prod.Count).GreaterThan(0);

            RuleFor(prod => prod.Description).NotNull();
            RuleFor(prod => prod.Description).MinimumLength(20);
            RuleFor(prod => prod.Description).MaximumLength(500);

            RuleFor(prod => prod.Categories).NotNull();
        }
    }
}
