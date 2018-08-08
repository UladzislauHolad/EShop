using EShop.App.Web.Controllers;
using EShop.Data.EF;
using EShop.Data.Entities;
using EShop.Data.Interfaces;
using EShop.Data.Repositories;
using EShop.Services.Interfaces;
using EShop.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using AutoMapper;
using EShop.App.Web.Models;
using EShop.Services.DTO;

namespace EShop.App.Web.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void IndexViewResultIsNotNull()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ProductDTO, ProductViewModel>();
                cfg.CreateMap<ProductViewModel, ProductDTO>();
            });
            var mapper = new Mapper(config);

            HomeController controller = new HomeController(GetService(), mapper);

            ViewResult result = controller.Index() as ViewResult;

            Assert.NotNull(result);
        }

        private IService GetService()
        {
            var mock = new Mock<IRepository<Product>>();
            mock.Setup(repo => repo.GetAll()).Returns(GetProducts());
            return new Service(mock.Object);
        }


        private IEnumerable<Product> GetProducts()
        {
            List<Product> products = new List<Product>
            {
                new Product { ProductId = 1, Name = "P21", Description = "Des21", Price = 21 },
                new Product { ProductId = 2, Name = "P22", Description = "Des22", Price = 22 },
                new Product { ProductId = 3, Name = "P23", Description = "Des23", Price = 23 },
                new Product { ProductId = 4, Name = "P24", Description = "Des24", Price = 24 },
                new Product { ProductId = 5, Name = "P25", Description = "Des25", Price = 25 }
            };

            return products;
        }
    }
}
