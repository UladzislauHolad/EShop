using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.App.Web.Models.ModelValidators
{
    public class NewOrderCountValidator : AbstractValidator<NewOrderCount>
    {
        public NewOrderCountValidator()
        {
            RuleFor(noc => noc.OrderCount).NotNull();
            RuleFor(noc => noc.OrderCount).GreaterThan(-1);
            RuleFor(noc => noc.MaxCount).NotNull();
            RuleFor(noc => noc.MaxCount).GreaterThanOrEqualTo(noc => noc.OrderCount);
        }
    }
}
