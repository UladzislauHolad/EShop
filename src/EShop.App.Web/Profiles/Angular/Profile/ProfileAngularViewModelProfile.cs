using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EShop.App.Web.Models.Angular.Profile;
using EShop.Services.DTO;

namespace EShop.App.Web.Profiles.Angular.ProfileViewModels
{
    public class ProfileAngularViewModelProfile : Profile
    {
        public ProfileAngularViewModelProfile()
        {
            CreateMap<UserDTO, ProfileAngularViewModel>();
            CreateMap<UserDTO, ProfileAngularViewModel>().ReverseMap();
        }
    }
}
