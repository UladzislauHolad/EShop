using EShop.Api.Models.OrdersViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Api.Validators.Orders
{
    public class CustomerViewModelValidator : AbstractValidator<CustomerViewModel>
    {
        public CustomerViewModelValidator()
        {
            RuleFor(c => c.FirstName).NotEmpty();
            RuleFor(c => c.FirstName).MinimumLength(2);
            RuleFor(c => c.FirstName).MaximumLength(20);

            RuleFor(c => c.LastName).NotEmpty();
            RuleFor(c => c.LastName).MinimumLength(2);
            RuleFor(c => c.LastName).MaximumLength(20);

            RuleFor(c => c.Patronymic).NotEmpty();
            RuleFor(c => c.Patronymic).MinimumLength(2);
            RuleFor(c => c.Patronymic).MaximumLength(20);

            RuleFor(c => c.Phone).NotEmpty();
            //RuleFor(c => c.Phone).Matches(@"/\(?([0-9]{3})\)?([ .-]?)([0-9]{3})\2([0-9]{4})/");

            RuleFor(c => c.Address).NotEmpty();
            RuleFor(c => c.Address).MinimumLength(10);
            RuleFor(c => c.Address).MaximumLength(50);
        }
    }
}
