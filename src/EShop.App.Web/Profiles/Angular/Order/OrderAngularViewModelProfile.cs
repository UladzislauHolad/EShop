using AutoMapper;
using EShop.App.Web.Models.Angular;
using EShop.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.App.Web.Profiles.Angular
{
    public class OrderAngularViewModelProfile : Profile
    {
        public OrderAngularViewModelProfile()
        {
            CreateMap<OrderDTO, OrderAngularViewModel>()
                .ForMember(dest => dest.DeliveryMethodName,
                opt => opt.MapFrom(src => src.DeliveryMethod.Name))
                .ForMember(dest => dest.PaymentMethodName,
                opt => opt.MapFrom(src => src.PaymentMethod.Name));

            CreateMap<OrderAngularViewModel, OrderDTO>();
        }
    }
}
