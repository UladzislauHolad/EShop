using Arch.IS4Host.Models;
using AutoMapper;
using IdentityModel;
using IdentityServer4.Quickstart.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        public async Task<ActionResult> DeleteUserAsync([FromRoute]string name)
        {
            var claim = User.Claims.SingleOrDefault(c => c.Type == JwtClaimTypes.ClientId);
            var users = await _userManager.GetUsersForClaimAsync(claim);
            var deletingUser = users.SingleOrDefault(u => u.UserName == name);

            var result = await _userManager.DeleteAsync(deletingUser);

            if (result.Succeeded)
            {
                return StatusCode(StatusCodes.Status201Created);
            }

            return StatusCode(StatusCodes.Status422UnprocessableEntity, result.Errors.Select(e => e.Description).ToList());
        }

        [HttpPost("users")]
        public async Task<ActionResult> CreateUserAsync([FromBody]CreateUserModel userModel)
        {
            var clientIdClaim = User.Claims.SingleOrDefault(c => c.Type == JwtClaimTypes.ClientId);
            var user = new ApplicationUser
            {
                UserName = userModel.UserName,
                Email = userModel.Email
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);

            if(result.Succeeded)
            {
                await _userManager.AddClaimAsync(user, clientIdClaim);
                return StatusCode(StatusCodes.Status201Created);
            }

            return StatusCode(StatusCodes.Status422UnprocessableEntity,result.Errors.Select(e => e.Description).ToList());
        }
    }
}
