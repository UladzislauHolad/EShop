using AutoMapper;
using EShop.Data.Entities;
using EShop.Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Services.Profiles
{
    public class DeliveryMethodDTOProfile : Profile
    {
        public DeliveryMethodDTOProfile()
        {
            CreateMap<DeliveryMethodDTO, DeliveryMethod>();
            CreateMap<DeliveryMethod, DeliveryMethodDTO>();
        }
    }
}
