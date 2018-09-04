using EShop.Data.Entities;
using EShop.Services.DTO;
using EShop.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace EShop.Services.Tests
{
    public class AccountServiceTests
    {
        [Fact]
        public void GetUsers_Invoke_ReturnIEnumerableUserDTO()
        {
            var mgr = MockUserManager<User>();
            mgr.Setup(m => m.Users).Returns(new List<User>().AsQueryable());
            var smgr = MockSignInManager(mgr.Object);
            var service = new AccountService(mgr.Object, smgr.Object);

            var result = service.GetUsers();

            Assert.NotNull(result);
            Assert.True(result is IEnumerable<UserDTO>);
        }

        [Fact]
        public async Task CreateUserAsync_InvokeWithValidUser_ReturnIdentitySucceeded()
        {
            UserDTO userDTO = new UserDTO
            {
                UserName = "user",
                Email = "email@email.da"
            };
            User user = new User
            {
                UserName = userDTO.UserName,
                Email = userDTO.Email
            };
            string password = "password";
            var mgr = MockUserManager<User>();
            mgr.Setup(m => m.CreateAsync(
                It.Is<User>(u => u.UserName == userDTO.UserName && u.Email == userDTO.Email), password))
                .Returns(Task.FromResult(IdentityResult.Success));
            var smgr = MockSignInManager(mgr.Object);
            var service = new AccountService(mgr.Object, smgr.Object);

            var result = await service.CreateUserAsync(userDTO, password);

            Assert.NotNull(result);
            Assert.True(result.Succeeded);
        }

        [Fact]
        public async Task DeleteUserAsync_InvokeWithValidId_ReturnIdentitySucceeded()
        {
            string id = "id";
            User user = new User
            {
                Id = id
            };
            var mgr = MockUserManager<User>();
            mgr.Setup(m => m.DeleteAsync(It.Is<User>(u => u.Id == id)))
                .Returns(Task.FromResult(IdentityResult.Success));
            mgr.Setup(m => m.FindByIdAsync(id)).
                Returns(Task.FromResult(user));
            var smgr = MockSignInManager(mgr.Object);
            var service = new AccountService(mgr.Object, smgr.Object);

            var result = await service.DeleteUserAsync(id);

            Assert.NotNull(result);
            Assert.True(result.Succeeded);
        }

        [Fact]
        public async Task DeleteUserAsync_InvokeWithNotValidId_ReturnIdentityNull()
        {
            string id = "id";
            User user = null;
            var mgr = MockUserManager<User>();
            mgr.Setup(m => m.FindByIdAsync(id)).
                Returns(Task.FromResult(user));
            var smgr = MockSignInManager(mgr.Object);
            var service = new AccountService(mgr.Object, smgr.Object);

            var result = await service.DeleteUserAsync(id);

            Assert.Null(result);
        }

        [Fact]
        public async Task FindByIdAsync_InvokeWithValidId_ReturnIdentitySucceeded()
        {
            string id = "id";
            User user = new User
            {
                Id = id
            };
            var mgr = MockUserManager<User>();
            mgr.Setup(m => m.FindByIdAsync(id)).
                Returns(Task.FromResult(user));
            var smgr = MockSignInManager(mgr.Object);
            var service = new AccountService(mgr.Object, smgr.Object);

            var result = await service.FindByIdAsync(id);

            Assert.NotNull(result);
            Assert.True(result is UserDTO);
        }

        [Fact]
        public async Task FindByIdAsync_InvokeWithNotValidId_ReturnNull()
        {
            string id = "id";
            User user = null;
            var mgr = MockUserManager<User>();
            mgr.Setup(m => m.FindByIdAsync(id)).
                Returns(Task.FromResult(user));
            var smgr = MockSignInManager(mgr.Object);
            var service = new AccountService(mgr.Object, smgr.Object);

            var result = await service.FindByIdAsync(id);

            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateUserAsync_InvokeWithValid_ReturnIdentitySucceeded()
        {
            string id = "id";
            User user = new User
            {
                Id = id
            };
            UserDTO userDTO = new UserDTO
            {
                Id = id
            };
            var mgr = MockUserManager<User>();
            mgr.Setup(m => m.UpdateAsync(It.Is<User>(u => u.Id == id))).
                Returns(Task.FromResult(IdentityResult.Success));
            var smgr = MockSignInManager(mgr.Object);
            var service = new AccountService(mgr.Object, smgr.Object);

            var result = await service.UpdateUserAsync(userDTO);

            Assert.NotNull(result);
            Assert.True(result.Succeeded);
        }

        [Fact]
        public async Task FindByEmailAsync_InvokeWithValidEmail_ReturnIdentitySucceeded()
        {
            string email = "email@.email.ru";
            User user = new User
            {
                Email = email
            };
            var mgr = MockUserManager<User>();
            mgr.Setup(m => m.FindByEmailAsync(email)).
                Returns(Task.FromResult(user));
            var smgr = MockSignInManager(mgr.Object);
            var service = new AccountService(mgr.Object, smgr.Object);

            var result = await service.FindByEmailAsync(email);

            Assert.NotNull(result);
            Assert.True(result is UserDTO);
        }

        [Fact]
        public async Task FindByEmailAsync_InvokeWithNotValidEmail_ReturnNull()
        {
            string email = "email@.email.ru";
            User user = null;
            var mgr = MockUserManager<User>();
            mgr.Setup(m => m.FindByEmailAsync(email)).
                Returns(Task.FromResult(user));
            var smgr = MockSignInManager(mgr.Object);
            var service = new AccountService(mgr.Object, smgr.Object);

            var result = await service.FindByEmailAsync(email);

            Assert.Null(result);
        }

        [Fact]
        public async Task HasPasswordAsync_InvokeWithValidNotId_ReturnBoolean()
        {
            string id = "id";
            User user = new User
            {
                Id = id
            };
            var mgr = MockUserManager<User>();
            mgr.Setup(m => m.FindByIdAsync(id)).
                Returns(Task.FromResult(user));
            var smgr = MockSignInManager(mgr.Object);
            var service = new AccountService(mgr.Object, smgr.Object);

            var result = await service.HasPasswordAsync(id);

            Assert.True(result is Boolean);
        }

        [Fact]
        public async Task HasPasswordAsync_InvokeWithNotValidId_ReturnNullReferenceException()
        {
            string id = "id";
            User user = null;
            var mgr = MockUserManager<User>();
            mgr.Setup(m => m.FindByIdAsync(id)).
                Returns(Task.FromResult(user));
            var smgr = MockSignInManager(mgr.Object);
            var service = new AccountService(mgr.Object, smgr.Object);

            await Assert.ThrowsAsync<NullReferenceException>(async () => { await service.HasPasswordAsync(id); });
        }

        [Fact]
        public async Task SignInAsync_InvokeWithNotValidId_ReturnNullReferenceException()
        {
            string id = "id";
            bool isPersistent = false;
            User user = null;
            var mgr = MockUserManager<User>();
            mgr.Setup(m => m.FindByIdAsync(id)).
                Returns(Task.FromResult(user));
            var smgr = MockSignInManager(mgr.Object);
            var service = new AccountService(mgr.Object, smgr.Object);

            await Assert.ThrowsAsync<NullReferenceException>(async () => 
            {
                await service.SignInAsync(id, isPersistent);
            });
        }

        [Fact]
        public void SignInAsync_InvokeWithValidId_ReturnIdentityResult()
        {
            string id = "id";
            bool isPersistent = false;
            User user = new User
            {
                Id = id
            };
            var mgr = MockUserManager<User>();
            mgr.Setup(m => m.FindByIdAsync(id)).
                Returns(Task.FromResult(user));
            var smgr = MockSignInManager(mgr.Object);
            smgr.Setup(m => m.SignInAsync(It.Is<User>(u => u.Id == user.Id), isPersistent, null))
                .Returns(Task.FromResult(IdentityResult.Success));
            var service = new AccountService(mgr.Object, smgr.Object);

            var result = service.SignInAsync(id, isPersistent).GetAwaiter().IsCompleted;

            Assert.True(result);
        }

        [Fact]
        public async Task ChangePasswordAsync_InvokeWithNotValidId_ReturnNullReferenceException()
        {
            string id = "id";
            string oldPassword = "old";
            string newPassword = "new";
            User user = null;
            var mgr = MockUserManager<User>();
            mgr.Setup(m => m.FindByIdAsync(id))
                .Returns(Task.FromResult(user));
            var smgr = MockSignInManager(mgr.Object);
            var service = new AccountService(mgr.Object, smgr.Object);

            await Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                await service.ChangePasswordAsync(id, oldPassword, newPassword);
            });
        }

        [Fact]
        public async Task ChangePasswordAsync_InvokeWithValid_ReturnIdentityResult()
        {
            string id = "id";
            string oldPassword = "old";
            string newPassword = "new";
            User user = new User
            {
                Id = id
            };
           
            var mgr = MockUserManager<User>();
            mgr.Setup(m => m.FindByIdAsync(id))
                .Returns(Task.FromResult(user));
            mgr.Setup(m => m.ChangePasswordAsync(It.Is<User>(u => u.Id == user.Id), oldPassword, newPassword))
                .Returns(Task.FromResult(IdentityResult.Success));
            var smgr = MockSignInManager(mgr.Object);
            var service = new AccountService(mgr.Object, smgr.Object);

            var result = await service.ChangePasswordAsync(id, oldPassword, newPassword);

            Assert.True(result is IdentityResult);
        }

        [Fact]
        public async Task GetUserAsync_InvokeNotNullClaimsPrincipal_ReturnUserDTO()
        {
            var claims = new ClaimsPrincipal();
            User user = new User();
            var mgr = MockUserManager<User>();
            mgr.Setup(m => m.GetUserAsync(claims))
                .Returns(Task.FromResult(user));
            var smgr = MockSignInManager(mgr.Object);
            var service = new AccountService(mgr.Object, smgr.Object);

            var result = await service.GetUserAsync(claims);

            Assert.True(result is UserDTO);
        }

        [Fact]
        public async Task PasswordSignInAsync_InvokeWithValidParams_ReturnSignInResult()
        {
            string userName = "user";
            string password = "password";
            bool isPersistent = true;
            var mgr = MockUserManager<User>();
            var smgr = MockSignInManager(mgr.Object);
            smgr.Setup(m => m.PasswordSignInAsync(userName, password, isPersistent, false))
                .Returns(Task.FromResult(SignInResult.Success));
            var service = new AccountService(mgr.Object, smgr.Object);

            var result = await service.PasswordSignInAsync(userName, password, isPersistent, false);

            Assert.True(result is SignInResult);
        }

        [Fact]
        public void SignOutAsync_Invoke_TaskisCompleted()
        {
            var mgr = MockUserManager<User>();
            var smgr = MockSignInManager(mgr.Object);
            smgr.Setup(m => m.SignOutAsync())
                .Returns(Task.CompletedTask);
            var service = new AccountService(mgr.Object, smgr.Object);

            var result = service.SignOutAsync().IsCompletedSuccessfully;

            Assert.True(result);
        }

        [Fact]
        public async Task ResetPasswordAsync_InvokeWithNotValidId_ReturnNullReferenceException()
        {
            string id = "id";
            string code = "code";
            string newPassword = "new";
            User user = null;
            var mgr = MockUserManager<User>();
            mgr.Setup(m => m.FindByIdAsync(id))
                .Returns(Task.FromResult(user));
            var smgr = MockSignInManager(mgr.Object);
            var service = new AccountService(mgr.Object, smgr.Object);

            await Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                await service.ResetPasswordAsync(id, code, newPassword);
            });
        }

        [Fact]
        public async Task ResetPasswordAsync_InvokeWithValid_ReturnIdentityResult()
        {
            string id = "id";
            string code = "code";
            string newPassword = "new";
            User user = new User
            {
                Id = id
            };

            var mgr = MockUserManager<User>();
            mgr.Setup(m => m.FindByIdAsync(id))
                .Returns(Task.FromResult(user));
            mgr.Setup(m => m.ResetPasswordAsync(It.Is<User>(u => u.Id == user.Id), code, newPassword))
                .Returns(Task.FromResult(IdentityResult.Success));
            var smgr = MockSignInManager(mgr.Object);
            var service = new AccountService(mgr.Object, smgr.Object);

            var result = await service.ResetPasswordAsync(id, code, newPassword);

            Assert.True(result is IdentityResult);
        }

        public static Mock<UserManager<TUser>> MockUserManager<TUser>() where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            var mgr = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<TUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());
            return mgr;
        }

        public static Mock<SignInManager<TUser>> MockSignInManager<TUser>(UserManager<TUser> umgr) where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            var mgr = new Mock<SignInManager<TUser>>(umgr, 
                new Mock<IHttpContextAccessor>().Object, 
                new Mock<IUserClaimsPrincipalFactory<TUser>>().Object, 
                null, null, null);
            return mgr;
        }
    }
}
