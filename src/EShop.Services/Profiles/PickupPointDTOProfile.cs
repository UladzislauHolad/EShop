using AutoMapper;
using EShop.Data.Entities;
using EShop.Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Services.Profiles
{
    public class PickupPointDTOProfile : Profile
    {
        public PickupPointDTOProfile()
        {
            CreateMap<PickupPoint, PickupPointDTO>();
            CreateMap<PickupPointDTO, PickupPoint>();
        }
    }
}
