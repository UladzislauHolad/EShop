using EShop.Api.Models.OrdersViewModels;
using EShop.Services.Infrastructure.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Api.Validators.Orders
{
    public class ModifyOrderViewModelValidator : AbstractValidator<ModifyOrderViewModel>
    {
        public ModifyOrderViewModelValidator()
        {
            RuleFor(order => order.Date).NotEmpty();

            RuleFor(order => order.Status).NotEmpty();

            RuleFor(order => order.Customer).NotNull();
            RuleFor(order => order.Customer).SetValidator(new CustomerViewModelValidator());

            RuleFor(order => order.DeliveryMethodId).NotEmpty();

            RuleFor(order => order.PaymentMethodId).NotEmpty();

            RuleFor(order => order.PickupPointId)
                .Must(p => p != 0).When(order => order.DeliveryMethodId == 1)
                .WithMessage("This delivery method must have pickup point");

            RuleFor(order => order.Comment).NotEmpty();
            RuleFor(order => order.Comment).MinimumLength(10);
            RuleFor(order => order.Comment).MaximumLength(50);
        }
    }
}
