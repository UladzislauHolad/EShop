using AutoMapper;
using EShop.App.Web.Models.Angular.User;
using EShop.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.App.Web.Profiles.Angular.User
{
    public class UserInfoAngularViewModelProfile : Profile
    {
        public UserInfoAngularViewModelProfile() 
        {
            CreateMap<UserDTO, UserInfoAngularViewModel>();
            CreateMap<UserDTO, UserInfoAngularViewModel>().ReverseMap();
        }
    }
}
