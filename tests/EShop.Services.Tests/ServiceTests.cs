using AutoMapper;
using EShop.Data.Entities;
using EShop.Data.Interfaces;
using EShop.Services.DTO;
using EShop.Services.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace EShop.Services.Tests
{
    public class ServiceTests
    {
        [Fact]
        public void CheckMapping()
        {
            var mock = new Mock<IRepository<Product>>();
            var products = GetProducts();
            mock.Setup(repo => repo.GetAll()).Returns(products);
            Service service = new Service(mock.Object);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductDTO>()).CreateMapper();
            var expected = mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(products);

            var result = service.GetProducts();

            Assert.True(expected.SequenceEqual(result, new ProductDTOComparer()));
        }

        [Fact]
        public void CheckFormat()
        {
            var mock = new Mock<IRepository<Product>>();
            var products = GetProducts();
            mock.Setup(repo => repo.GetAll()).Returns(products);
            Service service = new Service(mock.Object);

            var result = service.GetProducts();

            Assert.IsAssignableFrom<IEnumerable<ProductDTO>>(result);
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
