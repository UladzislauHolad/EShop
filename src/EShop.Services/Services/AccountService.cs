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
        private readonly IMapper _mapper;

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            var userDTOs = _userManager.Users.AsNoTracking().ToList();

            return _mapper.Map<IEnumerable<UserDTO>>(userDTOs);
        }

        public async Task<IEnumerable<UserDTO>> GetUsersAsync()
        {
            var userDTOs = await _userManager.Users.ToListAsync();

            return _mapper.Map<IEnumerable<UserDTO>>(userDTOs);
        }

        public async Task<IdentityResult> CreateUserAsync(UserDTO user, string password)
        {
            var result = await _userManager.CreateAsync(_mapper.Map<User>(user), password);

            return result;
        }

        public async Task<IdentityResult> DeleteUserByNameAsync(string name)
        {
            IdentityResult result = null;

            var existUser = await _userManager.FindByNameAsync(name);
            
            if(existUser != null)
            {
                await _userManager.UpdateSecurityStampAsync(existUser);
                result = await _userManager.DeleteAsync(existUser);
            }

            return result;
        }

        public async Task<IdentityResult> DeleteUserAsync(string id)
        {
            IdentityResult result = null;

            var existUser = await _userManager.FindByIdAsync(id);

            if (existUser != null)
            {
                await _userManager.UpdateSecurityStampAsync(existUser);
                result = await _userManager.DeleteAsync(existUser);
            }

            return result;
        }
        
        public async Task<UserDTO> FindByIdAsync(string id)
        {
            var user = _mapper.Map<UserDTO>(await _userManager.FindByIdAsync(id));

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
            return _userManager.UpdateAsync(_mapper.Map<User>(user));
        }

        public async Task<UserDTO> FindByEmailAsync(string email)
        {
            return _mapper.Map<UserDTO>(await _userManager.FindByEmailAsync(email));
        }

        public async Task<IdentityResult> ResetPasswordAsync(string id, string code, string newPassword)
        {
            var existUser = await _userManager.FindByIdAsync(id);

            if (existUser == null)
                throw new NullReferenceException();

            return await _userManager.ResetPasswordAsync(existUser, code, newPassword);
        }

        public async Task<UserDTO> GetUserAsync(ClaimsPrincipal principal)
        {
            return _mapper.Map<UserDTO>(await _userManager.GetUserAsync(principal));
        }

        public async Task<IdentityResult> ChangePasswordAsync(string id, string oldPassword, string newPassword)
        {
            var existUser = await _userManager.FindByIdAsync(id);

            if (existUser == null)
                throw new NullReferenceException();

            return await _userManager.ChangePasswordAsync(existUser, oldPassword, newPassword);
        }

        public async Task SignInByIdAsync(string id, bool isPersistent, string authenticationMethod = null)
        {
            var existUser = await _userManager.FindByIdAsync(id);

            if (existUser == null)
                throw new NullReferenceException();

            await _signInManager.SignInAsync(existUser, isPersistent);
        }

        public async Task SignInAsync(UserDTO user, bool isPersistent, string authenticationMethod = null)
        {
            var existUser = await _userManager.FindByEmailAsync(user.Email);

            if (existUser == null)
                throw new NullReferenceException();

            await _signInManager.SignInAsync(existUser, isPersistent);
        }

        public async Task<bool> HasPasswordAsync(string id)
        {
            var existUser = await _userManager.FindByIdAsync(id);

            if (existUser == null)
                throw new NullReferenceException();

            return await _userManager.HasPasswordAsync(existUser);
        }

        public async Task<UserDTO> FindByNameAsync(string userName)
        {
            return _mapper.Map<UserDTO>(await _userManager.FindByNameAsync(userName));
        }
    }
}
