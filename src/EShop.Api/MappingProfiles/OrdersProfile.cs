using AutoMapper;
using EShop.Api.Models.OrdersViewModels;
using EShop.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Api.MappingProfiles
{
    public class OrdersProfile : Profile
    {
        public OrdersProfile()
        {
            CreateMap<OrderDTO, OrderViewModel>();
            CreateMap<OrderDTO, OrderViewModel>().ReverseMap();

            CreateMap<ModifyOrderViewModel, OrderDTO>();
            CreateMap<ModifyOrderViewModel, OrderDTO>().ReverseMap();

            CreateMap<CustomerViewModel, CustomerDTO>();
            CreateMap<CustomerViewModel, CustomerDTO>().ReverseMap();
        }
    }
}
