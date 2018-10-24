using AutoMapper;
using EShop.Api.Models.ProductOrdersViewModels;
using EShop.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Api.MappingProfiles
{
    public class ProductOrdersProfile : Profile
    {
        public ProductOrdersProfile()
        {
            CreateMap<ProductOrderViewModel, ProductOrderDTO>();
            CreateMap<ProductOrderDTO, ProductOrderViewModel>()
                .ForMember(dest => dest.MaxCount,
                    opt => opt.MapFrom(src => src.OrderCount + src.Product.Count));

            CreateMap<CreateProductOrderViewModel, ProductOrderDTO>();
            CreateMap<CreateProductOrderViewModel, ProductOrderDTO>().ReverseMap();
        }
    }
}
