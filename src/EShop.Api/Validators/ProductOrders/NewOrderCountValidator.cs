using EShop.Api.Models.ProductOrdersViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Api.Validators.ProductOrders
{
    public class NewOrderCountValidator : AbstractValidator<NewOrderCount>
    {
        public NewOrderCountValidator()
        {
            RuleFor(noc => noc.MaxCount).GreaterThan(0);

            RuleFor(noc => noc.OrderCount).GreaterThan(-1);
            RuleFor(noc => noc.OrderCount).LessThanOrEqualTo(noc => noc.MaxCount);
        }
    }
}
