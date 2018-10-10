using Arch.IS4Host.Models;
using AutoMapper;
using IdentityModel;
using IdentityServer4.Quickstart.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Eshop.Auth.Quickstart.Users
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;


        public UsersController(
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet("users")]
        public async Task<IEnumerable<UserTableInfo>> GetUserAsync()
        {
            var claim = User.Claims.SingleOrDefault(c => c.Type == JwtClaimTypes.ClientId);
            var users = await _userManager.GetUsersForClaimAsync(claim);
            
            return _mapper.Map<IEnumerable<UserTableInfo>>(users);
        }


        [HttpDelete("users/{name}")]
        public async Task<bool> DeleteUserAsync([FromRoute]string name)
        {
            var claim = User.Claims.SingleOrDefault(c => c.Type == JwtClaimTypes.ClientId);
            var users = await _userManager.GetUsersForClaimAsync(claim);
            var deletingUser = users.SingleOrDefault(u => u.UserName == name);

            var result = await _userManager.DeleteAsync(deletingUser);

            return result.Succeeded;
        }
    }
}
