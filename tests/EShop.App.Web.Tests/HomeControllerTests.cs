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

namespace EShop.App.Web.Tests
{
    public class HomeControllerTests
    {
        private IService GetService()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=productdb;Trusted_Connection=True;");
            ProductContext context = new ProductContext(optionsBuilder.Options);
            ProductRepository repository = new ProductRepository(context);
            Service service = new Service(repository);
            return service;
        }

        [Fact]
        public void IndexViewResultIsNotNull()
        {
            ////HomeController controller = new HomeController(GetService());

            //ViewResult result = controller.Index() as ViewResult;

            //Assert.NotNull(result);
        }

        [Fact]
        public void IndexModelItemAtIndex()
        {
            var mock = new Mock<IRepository<Product>>();
            mock.Setup(repo => repo.GetAll()).Returns(new[] {
                new Product { ProductId = 1, Name = "P21", Description = "Des21", Price = 21 },
                new Product { ProductId = 2, Name = "P22", Description = "Des22", Price = 22 }
            });
            //HomeController controller = new HomeController(new Service(mock.Object));

            //ViewResult result = controller.Index() as ViewResult;
            //var prods = result.Model as IEnumerable<Product>;

            Service service = new Service(mock.Object);
            var colection = service.GetProducts();

            //Assert.Equal("P21", );
        }
    }
}
