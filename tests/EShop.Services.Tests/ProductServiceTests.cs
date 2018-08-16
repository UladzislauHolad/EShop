using AutoMapper;
using EShop.Data.Entities;
using EShop.Data.Interfaces;
using EShop.Services.DTO;
using EShop.Services.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace EShop.Services.Tests
{
    public class ProductServiceTests
    {
        [Fact]
        public void GetProducts_MappingProductToProductDTO_ProductToProductDTOMapped()
        {
            var products = GetProducts();
            ProductService service = GetService(products);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductDTO>()).CreateMapper();
            var expected = mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(products);

            var result = service.GetProducts();

            Assert.True(expected.SequenceEqual(result));
        }

        [Fact]
        public void GetProducts_IsResultHaveIEnumerableProductDTOType_ResultIsIEnumerableProductDTO()
        {
            ProductService service = GetService(GetProducts());

            var result = service.GetProducts();

            Assert.IsAssignableFrom<IEnumerable<ProductDTO>>(result);
        }

        [Fact]
        public void GetProducts_CheckCountOfProductsInRepositoryAndFromService_CountsOfProductsAreMatch()
        {
            var products = GetProducts();
            int expected = products.Count();
            ProductService service = GetService(products);

            int result = service.GetProducts().Count();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Delete_DeletingProductFromRepository_ProductDeleted()
        {
            const int id = 2;
            var mock = new Mock<IRepository<Product>>();
            mock.Setup(m => m.Delete(id));
            var service = new ProductService(mock.Object);

            service.Delete(id);

            mock.Verify(m => m.Delete(id), Times.Once());
        }

        [Fact]
        public void Add_CreateProductInRepository_ProductCreated()
        {
            var product = new Product { ProductId = 1, Name = "P21", Description = "Des21", Price = 21 };
            var mock = new Mock<IRepository<Product>>();
            mock.Setup(m => m.Create(product));
            var service = new ProductService(mock.Object);
            var mapper = GetMapper();

            service.Add(mapper.Map<Product, ProductDTO>(product));
            
            mock.Verify(m => m.Create(It.Is<Product>(p => p.Name == product.Name)), Times.Once());
        }

        [Fact]
        public void Update_UpdateProductInRepository_ProductUpdated()
        {
            var product = new Product { ProductId = 1, Name = "P21", Description = "Des21", Price = 21 };
            var mock = new Mock<IRepository<Product>>();
            mock.Setup(m => m.Update(product));
            var service = new ProductService(mock.Object);
            var mapper = GetMapper();

            service.Update(mapper.Map<Product, ProductDTO>(product));

            mock.Verify(m => m.Update(It.Is<Product>(p => p.Name == product.Name)), Times.Once());
        }

        private IQueryable<Product> GetProducts()
        {
            List<Product> products = new List<Product>
            {
                new Product { ProductId = 1, Name = "P21", Description = "Des21", Price = 21 },
                new Product { ProductId = 2, Name = "P22", Description = "Des22", Price = 22 },
                new Product { ProductId = 3, Name = "P23", Description = "Des23", Price = 23 },
                new Product { ProductId = 4, Name = "P24", Description = "Des24", Price = 24 },
                new Product { ProductId = 5, Name = "P25", Description = "Des25", Price = 25 }
            };

            return products.AsQueryable();
        }

        private IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ProductDTO, Product>();
                cfg.CreateMap<Product, ProductDTO>();
            });
            return new Mapper(config);
        }

        private ProductService GetService(IQueryable<Product> products)
        {
            var mock = new Mock<IRepository<Product>>();
            mock.Setup(repo => repo.GetAll()).Returns(products);
            return new ProductService(mock.Object);
        }
    }
}
