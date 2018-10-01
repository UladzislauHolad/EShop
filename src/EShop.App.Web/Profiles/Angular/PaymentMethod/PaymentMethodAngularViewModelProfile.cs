using AutoMapper;
using EShop.App.Web.Models.Angular.PaymentMethod;
using EShop.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.App.Web.Profiles.Angular.PaymentMethod
{
    public class PaymentMethodAngularViewModelProfile : Profile
    {
        public PaymentMethodAngularViewModelProfile()
        {
            CreateMap<PaymentMethodAngularViewModel, PaymentMethodDTO>();
            CreateMap<PaymentMethodAngularViewModel, PaymentMethodDTO>().ReverseMap();
        }
    }
}
