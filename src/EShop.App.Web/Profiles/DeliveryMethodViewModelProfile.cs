using AutoMapper;
using EShop.App.Web.Models.DeliveryMethodViewModels;
using EShop.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.App.Web.Profiles
{
    public class DeliveryMethodViewModelProfile : Profile
    {
        public DeliveryMethodViewModelProfile()
        {
            CreateMap<DeliveryMethodDTO, DeliveryMethodViewModel>();
            CreateMap<DeliveryMethodViewModel, DeliveryMethodDTO>();
        }
    }
}
