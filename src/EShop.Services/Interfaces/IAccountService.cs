using EShop.Services.DTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Services.Interfaces
{
    public interface IAccountService
    {
        IEnumerable<UserDTO> GetUsers();
        Task<UserDTO> FindByIdAsync(string id);
        Task<UserDTO> GetUserAsync(ClaimsPrincipal principal);
        Task<IdentityResult> DeleteUserAsync(string id);
        Task<IdentityResult> CreateUserAsync(UserDTO user, string password);
        Task<IdentityResult> UpdateUserAsync(UserDTO user);
        Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure);
        Task SignOutAsync();
        Task<UserDTO> FindByEmailAsync(string email);
        Task<UserDTO> FindByNameAsync(string userName);
        Task<IdentityResult> ResetPasswordAsync(string id, string code, string newPassword);
        Task<IdentityResult> ChangePasswordAsync(string id, string oldPassword, string newPassword);
        Task SignInByIdAsync(string id, bool isPersistent, string authenticationMethod = null);
        Task SignInAsync(UserDTO user, bool isPersistent, string authenticationMethod = null);
        Task<bool> HasPasswordAsync(string id);
    }
}
