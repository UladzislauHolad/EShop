using AutoMapper;
using EShop.App.Web.Controllers;
using EShop.Data.Entities;
using EShop.Services.DTO;
using EShop.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EShop.App.Web.Tests
{
    public class CatalogControllerTests
    {
        [Fact]
        public void Index_Invoke_ReturnView()
        {
            var service = new Mock<ICategoryService>();
            var mapper = GetMapper();
            var controller = new CatalogController(service.Object, mapper);

            var result = controller.Index();

            Assert.True(result is ViewResult);
        }

        private IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Category, CategoryDTO>();
                cfg.CreateMap<CategoryDTO, Category>();
            });

            return new Mapper(config);
        }
    }
}
