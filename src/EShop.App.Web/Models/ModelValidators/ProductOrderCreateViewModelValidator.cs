using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.App.Web.Models.ModelValidators
{
    public class ProductOrderCreateViewModelValidator : AbstractValidator<ProductOrderCreateViewModel>
    {
        public ProductOrderCreateViewModelValidator()
        {
            RuleFor(p => p.OrderCount).GreaterThan(0);
            RuleFor(p => p.OrderId).GreaterThan(0);
            RuleFor(p => p.ProductId).GreaterThan(0);
        }
    }
}
