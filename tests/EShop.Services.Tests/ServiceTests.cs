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
            var products = GetProducts();
            ProductService service = GetService(products);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductDTO>()).CreateMapper();
            var expected = mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(products);

            var result = service.GetProducts();

            Assert.True(expected.SequenceEqual(result));
        }

        [Fact]
        public void CheckFormat()
        {
            ProductService service = GetService(GetProducts());

            var result = service.GetProducts();

            Assert.IsAssignableFrom<IEnumerable<ProductDTO>>(result);
        }

        [Fact]
        public void CheckCount()
        {
            var products = GetProducts();
            int expected = products.Count();
            ProductService service = GetService(products);

            int result = service.GetProducts().Count();

            Assert.Equal(expected, result);
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

        private ProductService GetService(IEnumerable<Product> products)
        {
            var mock = new Mock<IRepository<Product>>();
            mock.Setup(repo => repo.GetAll()).Returns(products);
            return new ProductService(mock.Object);
        }

    }
}
