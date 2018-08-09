using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.App.Web.Models.ModelValidators
{
    public class ProductViewModelValidator : AbstractValidator<ProductViewModel>
    {
        public ProductViewModelValidator()
        {
            RuleFor(prod => prod.Name).NotEmpty();
            RuleFor(prod => prod.Price).GreaterThan(0);
            RuleFor(prod => prod.Description).NotEmpty();
        }
    }
}
