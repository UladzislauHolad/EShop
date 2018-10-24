using EShop.Api.Models.ProductOrdersViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Api.Validators.ProductOrders
{
    public class CreateProductOrderViewModelValidator : AbstractValidator<CreateProductOrderViewModel>
    {
        public CreateProductOrderViewModelValidator()
        {
            RuleFor(cpo => cpo.OrderId).NotEmpty();
            RuleFor(cpo => cpo.OrderCount).NotEmpty();
            RuleFor(cpo => cpo.ProductId).NotEmpty();
        }
    }
}
