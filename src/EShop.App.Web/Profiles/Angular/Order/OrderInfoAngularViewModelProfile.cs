using AutoMapper;
using EShop.App.Web.Models.Angular.Order;
using EShop.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.App.Web.Profiles.Angular.Order
{
    public class OrderInfoAngularViewModelProfile : Profile
    {
        public OrderInfoAngularViewModelProfile()
        {
            CreateMap<OrderInfoAngularViewModel, OrderDTO>();
            CreateMap<OrderDTO, OrderInfoAngularViewModel>();
        }
    }
}
