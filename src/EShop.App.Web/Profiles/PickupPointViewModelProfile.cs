using AutoMapper;
using EShop.App.Web.Models.PickupPointViewModels;
using EShop.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.App.Web.Profiles
{
    public class PickupPointViewModelProfile : Profile
    {
        public PickupPointViewModelProfile()
        {
            CreateMap<PickupPointViewModel, PickupPointDTO>();
            CreateMap<PickupPointDTO, PickupPointViewModel>();
        }
    }
}
