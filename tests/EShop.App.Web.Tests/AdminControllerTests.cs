using AutoMapper;
using EShop.App.Web.Controllers;
using EShop.App.Web.Models;
using EShop.App.Web.Profiles;
using EShop.Services.DTO;
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
    public class AdminControllerTests
    {
        [Fact]
        public void Index_Invoke_ViewResult()
        {
            var service = new Mock<IAccountService>();
            service.Setup(m => m.GetUsers()).Returns(new List<UserDTO>());
            var controller = new AdminController(service.Object, GetMapper());

            var result = controller.Index();

            Assert.True(result is ViewResult);
        }

        [Fact]
        public void Create_Invoke_ViewResultWithCreateUserViewModel()
        {
            var service = new Mock<IAccountService>();
            var controller = new AdminController(service.Object, GetMapper());

            var result = controller.Create();

            Assert.True(result is ViewResult);
            Assert.True((result as ViewResult).Model is CreateUserViewModel);
        }

        [Fact]
        public async Task Create_InvokeWithValidModel_RedirectToActionResult()
        {
            var model = new CreateUserViewModel
            {
                UserName = "user",
                Email = "user@mail.ru",
                Password = "qweQ_123DS"
            };
            var service = new Mock<IAccountService>();
            service.Setup(m => m.CreateUserAsync(It.Is<UserDTO>(x => x.UserName == model.UserName 
                    && x.Email == model.Email), model.Password))
                .Returns(Task.FromResult(IdentityResult.Success));
            var controller = new AdminController(service.Object, GetMapper());

            var result = await controller.Create(model);

            Assert.True(result is RedirectToActionResult);
        }

        [Fact]
        public async Task Create_InvokeWithNotValidModel_ViewResult()
        {
            var model = new CreateUserViewModel
            {
                UserName = "user",
                Email = "user@mail.ru",
                Password = "qweQ_123DS"
            };
            var service = new Mock<IAccountService>();
            service.Setup(m => m.CreateUserAsync(It.Is<UserDTO>(x => x.UserName == model.UserName
                    && x.Email == model.Email), model.Password))
                .Returns(Task.FromResult(IdentityResult.Failed()));
            var controller = new AdminController(service.Object, GetMapper());

            var result = await controller.Create(model);

            Assert.True(result is ViewResult);
        }

        [Fact]
        public async Task Delete_InvokeWithValidId_RedirectToActionResult()
        {
            var service = new Mock<IAccountService>();
            service.Setup(m => m.DeleteUserAsync("id"))
                .Returns(Task.FromResult(IdentityResult.Success));
            var controller = new AdminController(service.Object, GetMapper());

            var result = await controller.Delete("id");

            Assert.True(result is RedirectToActionResult);
        }

        [Fact]
        public async Task Delete_InvokeWithNotValidId_ViewResult()
        {
            var service = new Mock<IAccountService>();
            service.Setup(m => m.DeleteUserAsync("id"))
                .Returns(Task.FromResult(IdentityResult.Failed()));
            var controller = new AdminController(service.Object, GetMapper());

            var result = await controller.Delete("id");

            Assert.True(result is ViewResult);
        }

        private IMapper GetMapper()
        {
            var mapper = new MapperConfiguration(cfg =>
                cfg.AddProfile(new UserViewModelProfile())
                ).CreateMapper();

            return mapper;
        }
    }
}
