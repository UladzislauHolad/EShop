﻿using AutoMapper;
using EShop.App.Web.Models;
using EShop.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.App.Web.Profiles
{
    public class ProductOrderListViewModelProfile : Profile
    {
        public ProductOrderListViewModelProfile()
        {
            CreateMap<OrderDTO, ProductOrderListViewModel>();
            CreateMap<ProductOrderListViewModel, OrderDTO>();
        }
    }
}
