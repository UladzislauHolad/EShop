using AutoMapper;
using EShop.Data.Entities;
using EShop.Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Services.Profiles
{
    public class ProductOrderProfile : Profile
    {
        public ProductOrderProfile()
        {
            CreateMap<ProductOrder, ProductOrderDTO>();
            CreateMap<ProductOrderDTO, ProductOrder>();
        }
    }
}
