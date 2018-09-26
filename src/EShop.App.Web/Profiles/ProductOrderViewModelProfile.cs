using AutoMapper;
using EShop.App.Web.Models;
using EShop.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.App.Web.Profiles
{
    public class ProductOrderViewModelProfile : Profile
    {
        public ProductOrderViewModelProfile()
        {
            CreateMap<ProductOrderDTO, ProductOrderViewModel>()
                .ForMember(dest => dest.IsNotAvailable,
                opt => opt.MapFrom(src => src.Product.IsDeleted));

            CreateMap<ProductOrderViewModel, ProductOrderDTO>();
        }
    }
}
