﻿using AutoMapper;
using EShop.App.Web.Models.Angular;
using EShop.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.App.Web.Profiles.Angular
{
    public class ModifyOrderAngularViewModelProfile : Profile
    {
        public ModifyOrderAngularViewModelProfile()
        {
            CreateMap<ModifyOrderAngularViewModel, OrderDTO>();
            CreateMap<OrderDTO, ModifyOrderAngularViewModel>();
        }
    }
}
