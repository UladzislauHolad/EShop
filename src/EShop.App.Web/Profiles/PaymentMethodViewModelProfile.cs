using AutoMapper;
using EShop.App.Web.Models.PaymentMethodViewModels;
using EShop.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.App.Web.Profiles
{
    public class PaymentMethodViewModelProfile : Profile
    {
        public PaymentMethodViewModelProfile()
        {
            CreateMap<PaymentMethodViewModel, PaymentMethodDTO>();
            CreateMap<PaymentMethodDTO, PaymentMethodViewModel>();
        }
    }
}
