using AutoMapper;
using EShop.App.Web.Models.Angular.Account;
using EShop.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.App.Web.Profiles.Angular.Account
{
    public class UserAngularViewModelProfile : Profile
    {
        public UserAngularViewModelProfile()
        {
            CreateMap<UserDTO, UserAngularViewModel>();
            CreateMap<UserDTO, UserAngularViewModel>().ReverseMap();
        }
    }
}
