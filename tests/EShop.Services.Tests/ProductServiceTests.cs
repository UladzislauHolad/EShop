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
            var mock = new Mock<IRepository<Product>>();
            mock.Setup(repo => repo.GetAll()).Returns(new List<Product> { new Product() }.AsQueryable());
            ProductService service = new ProductService(mock.Object);

            var result = service.GetProducts();

            Assert.True(result is IEnumerable<ProductDTO>);
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
            ProductService service = GetService(products);

            int result = service.GetProducts().Count();

            Assert.Equal(4, result);
        }

        [Fact]
        public void Delete_DeletingProductFromRepository_ProductDeleted()
        {
            const int id = 2;
            var mock = new Mock<IRepository<Product>>();
            mock.Setup(m => m.Get(id)).Returns(new Product { ProductId = 1 });
            mock.Setup(m => m.Save());
            var service = new ProductService(mock.Object);

            service.Delete(id);

            mock.Verify(m => m.Get(id), Times.Once);
            mock.Verify(m => m.Save(), Times.Once);
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

        [Fact]
        public void GetCategoriesWithCountOfProducts_Invoke_ReturnsNameOfCategoriesAndCountOfProducts()
        {
            var mock = new Mock<IRepository<Product>>();
            mock.Setup(m => m.GetAll()).Returns(GetProducts());
            var service = new ProductService(mock.Object);

            var result = service.GetCategoriesWithCountOfProducts();

            Assert.Equal(2, result.Count);
        }

        private IQueryable<Product> GetProducts()
        {
            var category1 = new Category { CategoryId = 1, Name = "C1", ParentId = 0 };
            var category2 = new Category { CategoryId = 2, Name = "C2", ParentId = 0 };
            List<Product> products = new List<Product>
            {
                new Product { ProductId = 1, Name = "P21", Description = "Des21", Price = 21,
                    ProductCategories =
                        new List<ProductCategory>
                        {
                            new ProductCategory { ProductId = 1, CategoryId = 1, Category = category1 },
                            new ProductCategory { ProductId = 1, CategoryId = 2, Category = category2 },
                        }
                    },
                new Product { ProductId = 2, Name = "P22", Description = "Des22", Price = 22,
                    ProductCategories =
                        new List<ProductCategory>
                        {
                            new ProductCategory { ProductId = 2, CategoryId = 1, Category = category1 },
                            new ProductCategory { ProductId = 2, CategoryId = 2, Category = category2 },
                        }
                    },
                new Product { ProductId = 3, Name = "P23", Description = "Des23", Price = 23,
                    ProductCategories =
                        new List<ProductCategory>
                        {
                            new ProductCategory { ProductId = 3, CategoryId = 1, Category = category1 },
                            new ProductCategory { ProductId = 3, CategoryId = 2, Category = category2 },
                        }
                    },
                new Product { ProductId = 4, Name = "P24", Description = "Des24", Price = 24, IsDeleted = true,
                    ProductCategories =
                        new List<ProductCategory>
                        {
                            new ProductCategory { ProductId = 4, CategoryId = 1, Category = category1 },
                            new ProductCategory { ProductId = 4, CategoryId = 2, Category = category2 },
                        }
                    },
                new Product { ProductId = 5, Name = "P25", Description = "Des25", Price = 25, ProductCategories =
                        new List<ProductCategory>
                        {
                            new ProductCategory { ProductId = 5, CategoryId = 1, Category = category1 },
                            new ProductCategory { ProductId = 5, CategoryId = 2, Category = category2 },
                        }
                    }
            };

            return products.AsQueryable();
        }

        private IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
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
