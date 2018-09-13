using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.App.Web.Models.ModelValidators
{
    public class OrderViewModelValidator : AbstractValidator<OrderViewModel>
    {
        public OrderViewModelValidator()
        {
            RuleFor(o => o.Customer.FirstName).NotEmpty();
            RuleFor(o => o.Customer.LastName).NotEmpty();
            RuleFor(o => o.Customer.Patronymic).NotEmpty();
            RuleFor(o => o.Customer.Phone).NotEmpty().Matches(@"\(?([0-9]{3})\)?([ .-]?)([0-9]{3})\2([0-9]{4})");
            RuleFor(o => o.Customer.Address).NotEmpty();
            RuleFor(o => o.PaymentMethodId).GreaterThan(0).WithMessage("Choose exist method");
            RuleFor(o => o.DeliveryMethodId).GreaterThan(0).WithMessage("Choose exist method");
        }
    }
}
