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
            RuleFor(prod => prod.Customer.FirstName).NotEmpty();
            RuleFor(prod => prod.Customer.LastName).NotEmpty();
            RuleFor(prod => prod.Customer.Patronymic).NotEmpty();
            RuleFor(prod => prod.Customer.Phone).NotEmpty().Matches(@"\(?([0-9]{3})\)?([ .-]?)([0-9]{3})\2([0-9]{4})");
            RuleFor(prod => prod.Customer.Address).NotEmpty();
            RuleFor(prod => prod.Customer.Comment).NotEmpty();
        }
    }
}
