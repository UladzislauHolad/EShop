using AutoMapper;
using EShop.App.Web.Models.Angular.DeliveryMethod;
using EShop.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.App.Web.Profiles.Angular.DeliveryMethod
{
    public class DeliveryMethodAngularViewModelProfile : Profile
    {
        public DeliveryMethodAngularViewModelProfile()
        {
            CreateMap<DeliveryMethodDTO, DeliveryMethodAngularViewModel>();
            CreateMap<DeliveryMethodDTO, DeliveryMethodAngularViewModel>().ReverseMap();
        }
    }
}
