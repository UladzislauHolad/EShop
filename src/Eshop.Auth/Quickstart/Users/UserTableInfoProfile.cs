using Arch.IS4Host.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Auth.Quickstart.Users
{
    public class UserTableInfoProfile : Profile
    {
        public UserTableInfoProfile()
        {
            CreateMap<ApplicationUser, UserTableInfo>();
            CreateMap<ApplicationUser, UserTableInfo>().ReverseMap();
        }
    }
}
