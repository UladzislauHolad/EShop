using AutoMapper;
using EShop.Data.Entities;
using EShop.Services.DTO;
using EShop.Services.Interfaces;
using EShop.Services.Profiles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Services.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            var mapper = GetMapper();

            var userDTOs = _userManager.Users.AsNoTracking().ToList();

            return mapper.Map<IEnumerable<UserDTO>>(userDTOs);
        }

        public async Task<IdentityResult> CreateUserAsync(UserDTO user, string password)
        {
            var mapper = GetMapper();

            var result = await _userManager.CreateAsync(mapper.Map<User>(user), password);

            return result;
        }

        public async Task<IdentityResult> DeleteUserAsync(string id)
        {
            IdentityResult result = null;

            var existUser = await _userManager.FindByIdAsync(id);
            
            if(existUser != null)
            {
                await _userManager.UpdateSecurityStampAsync(existUser);
                result = await _userManager.DeleteAsync(existUser);
            }

            return result;
        }

        public async Task<UserDTO> FindByIdAsync(string id)
        {
            var mapper = GetMapper();

            var user = mapper.Map<UserDTO>(await _userManager.FindByIdAsync(id));

            return user;
        }

        public async Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
        {
            return await _signInManager.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure);
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public Task<IdentityResult> UpdateUserAsync(UserDTO user)
        {
            var mapper = GetMapper();

            return _userManager.UpdateAsync(mapper.Map<User>(user));
        }

        private IMapper GetMapper()
        {
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UserDTOProfile());
            }).CreateMapper();

            return mapper;
        }

        public async Task<UserDTO> FindByEmailAsync(string email)
        {
            var mapper = GetMapper();

            return mapper.Map<UserDTO>(await _userManager.FindByEmailAsync(email));
        }

        public async Task<IdentityResult> ResetPasswordAsync(string id, string code, string newPassword)
        {
            var mapper = GetMapper();

            var existUser = await _userManager.FindByIdAsync(id);

            return await _userManager.ResetPasswordAsync(existUser, code, newPassword);
        }

        public async Task<UserDTO> GetUserAsync(ClaimsPrincipal principal)
        {
            var mapper = GetMapper();

            return mapper.Map<UserDTO>(await _userManager.GetUserAsync(principal));
        }

        public async Task<IdentityResult> ChangePasswordAsync(string id, string oldPassword, string newPassword)
        {
            var mapper = GetMapper();

            var existUser = await _userManager.FindByIdAsync(id);

            return await _userManager.ChangePasswordAsync(existUser, oldPassword, newPassword);
        }

        public async Task SignInAsync(string id, bool isPersistent, string authenticationMethod = null)
        {
            var mapper = GetMapper();

            var existUser = await _userManager.FindByIdAsync(id);

            await _signInManager.SignInAsync(existUser, isPersistent);
        }

        public async Task<bool> HasPasswordAsync(string id)
        {
            var mapper = GetMapper();

            var existUser = await _userManager.FindByIdAsync(id);

            return await _userManager.HasPasswordAsync(existUser);
        }
    }
}
