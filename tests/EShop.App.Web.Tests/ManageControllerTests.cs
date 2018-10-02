using AutoMapper;
using EShop.App.Web.Controllers;
using EShop.App.Web.Models;
using EShop.App.Web.Profiles;
using EShop.Services.DTO;
using EShop.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Http;
using EShop.App.Web.Models.ManageViewModels;
using Microsoft.AspNetCore.Identity;

namespace EShop.App.Web.Tests
{
    public class ManageControllerTests
    {
        [Fact]
        public async Task Index_Invoke_ViewResult()
        {

            var user = new UserDTO
            {
                Id = "id"
            };
            var http = new Mock<HttpContext>();
            http.Setup(m => m.User).Returns(It.IsAny<ClaimsPrincipal>());
            var service = new Mock<IAccountService>();
            service.Setup(m => m.GetUserAsync(It.IsAny<ClaimsPrincipal>())).Returns(Task.FromResult(user));
            service.Setup(m => m.HasPasswordAsync("id")).Returns(Task.FromResult(true));
            var controller = new ManageController(service.Object, GetMapper());
            controller.ControllerContext.HttpContext = http.Object;

            var result = await controller.Index();

            Assert.True(result is ViewResult);
        }

        [Fact]
        public void ChangePassword_Invoke_ViewResult()
        {
            var service = new Mock<IAccountService>();
            var controller = new ManageController(service.Object, GetMapper());

            var result = controller.ChangePassword();

            Assert.True(result is ViewResult);
        }

        [Fact]
        public async Task ChangePassword_InvokeWithValidModel_RedirectToActionResult()
        {
            var user = new UserDTO
            {
                Id = "id"
            };
            var model = new ChangePasswordViewModel
            {
                ConfirmPassword = "123",
                NewPassword = "123",
                OldPassword = "321"
            };
            var http = new Mock<HttpContext>();
            http.Setup(m => m.User).Returns(It.IsAny<ClaimsPrincipal>());
            var service = new Mock<IAccountService>();
            service.Setup(m => m.GetUserAsync(It.IsAny<ClaimsPrincipal>())).Returns(Task.FromResult(user));
            service.Setup(m => m.ChangePasswordAsync(user.Id, model.OldPassword, model.NewPassword))
                .Returns(Task.FromResult(IdentityResult.Success));
            service.Setup(m => m.SignInByIdAsync(user.Id, false, null)).Returns(Task.CompletedTask);
            var controller = new ManageController(service.Object, GetMapper());
            controller.ControllerContext.HttpContext = http.Object;

            var result = await controller.ChangePassword(model);

            Assert.True(result is RedirectToActionResult);
        }


        [Fact]
        public async Task ChangePassword_InvokeWithNotValidModel_ViewResult()
        {
            var user = new UserDTO
            {
                Id = "id"
            };
            var model = new ChangePasswordViewModel
            {
                ConfirmPassword = "123",
                NewPassword = "123",
                OldPassword = "321"
            };
            var http = new Mock<HttpContext>();
            http.Setup(m => m.User).Returns(It.IsAny<ClaimsPrincipal>());
            var service = new Mock<IAccountService>();
            service.Setup(m => m.GetUserAsync(It.IsAny<ClaimsPrincipal>())).Returns(Task.FromResult(user));
            service.Setup(m => m.ChangePasswordAsync(user.Id, model.OldPassword, model.NewPassword))
                .Returns(Task.FromResult(IdentityResult.Failed()));
            service.Setup(m => m.SignInByIdAsync(user.Id, false, null)).Returns(Task.CompletedTask);
            var controller = new ManageController(service.Object, GetMapper());
            controller.ControllerContext.HttpContext = http.Object;

            var result = await controller.ChangePassword(model);

            Assert.True(result is ViewResult);
        }

        [Fact]
        public async Task ChangePassword_InvokeWithNullUser_RedirectToActionResult()
        {
            UserDTO user = null;
            var model = new ChangePasswordViewModel
            {
                ConfirmPassword = "123",
                NewPassword = "123",
                OldPassword = "321"
            };
            var http = new Mock<HttpContext>();
            http.Setup(m => m.User).Returns(It.IsAny<ClaimsPrincipal>());
            var service = new Mock<IAccountService>();
            service.Setup(m => m.GetUserAsync(It.IsAny<ClaimsPrincipal>())).Returns(Task.FromResult(user));
            var controller = new ManageController(service.Object, GetMapper());
            controller.ControllerContext.HttpContext = http.Object;

            var result = await controller.ChangePassword(model);

            Assert.True(result is RedirectToActionResult);
        }

        private IMapper GetMapper()
        {
            var mapper = new MapperConfiguration(cfg =>
                cfg.AddProfile(new UserViewModelProfile())
                ).CreateMapper();

            return mapper;
        }
    }

    //class MockHttpContext : HttpContextBase
    //{
    //    private readonly IPrincipal user;

    //    public MockHttpContext(IPrincipal principal)
    //    {
    //        this.user = principal;
    //    }

    //    public override IPrincipal User
    //    {
    //        get
    //        {
    //            return user;
    //        }
    //        set
    //        {
    //            base.User = value;
    //        }
    //    }
    //}
}
