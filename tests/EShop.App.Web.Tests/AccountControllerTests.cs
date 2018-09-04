using AutoMapper;
using EShop.App.Web.Controllers;
using EShop.App.Web.Models;
using EShop.App.Web.Profiles;
using EShop.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EShop.App.Web.Tests
{
    public class AccountControllerTests
    {
        [Fact]
        public void Login_LoginGetRequest_ReturnViewWithLoginViewModel()
        {
            var service = new Mock<IAccountService>();
            var controller = new AccountController(service.Object);

            var result = controller.Login();

            Assert.True(result is ViewResult);
            Assert.True((result as ViewResult).Model is LoginViewModel);
        }

        [Fact]
        public async Task Login_LoginWithValidModel_RedirectToIndexAsync()
        {
            var model = GetLoginViewModel();
            var service = new Mock<IAccountService>();
            service.Setup(m => m.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false))
                .Returns(Task.FromResult(Microsoft.AspNetCore.Identity.SignInResult.Success));
            var controller = new AccountController(service.Object);

            var result = await controller.Login(model);

            Assert.True(result is RedirectToActionResult);
            Assert.True((result as RedirectToActionResult).ActionName == "Index");
        }

        [Fact]
        public async Task Login_LoginWithNotValidModel_ViewResultAsync()
        {
            var model = GetLoginViewModel();
            var service = new Mock<IAccountService>();
            service.Setup(m => m.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false))
                .Returns(Task.FromResult(Microsoft.AspNetCore.Identity.SignInResult.Failed));
            var controller = new AccountController(service.Object);

            var result = await controller.Login(model);

            Assert.True(result is ViewResult);
        }

        [Fact]
        public async Task LogOff_Invoke_RedirectToIndexAsync()
        {
            var service = new Mock<IAccountService>();
            var controller = new AccountController(service.Object);

            var result = await controller.LogOff();

            Assert.True(result is RedirectToActionResult);
            Assert.True((result as RedirectToActionResult).ActionName == "Index");
        }

        private IMapper GetMapper()
        {
            var mapper = new MapperConfiguration(cfg =>
                cfg.AddProfile(new UserViewModelProfile())
                ).CreateMapper();

            return mapper;
        }

        private LoginViewModel GetLoginViewModel()
        {
            LoginViewModel model = new LoginViewModel
            {
                Email = "example@mail.ru",
                Password = "123421",
                RememberMe = false
            };

            return model;
        }
    }
}
