using EShop.Data.EF;
using EShop.Data.Entities;
using EShop.Data.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace EShop.Data.Tests
{
    public class ProductRepositoryTests
    {
        [Fact]
        public void Find_FindProductById_ProductIsFound()
        {
            var product = new Product { ProductId = 2, Name = "P22", Description = "Des22", Price = 22 };
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<EShopContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new EShopContext(options))
                {
                    context.Database.EnsureCreated();
                    context.Categories.AddRange(GetCategories());
                    context.Products.AddRange(GetProducts());
                    context.SaveChanges();
                }

                using (var context = new EShopContext(options))
                {
                    var repository = new ProductRepository(context);
                    var collection = repository.Find(p => p.ProductId == product.ProductId);

                    Assert.Single(collection);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public void GetAll_GetAllProducts_ProductsWithProductCategoryCollection()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            try
            {
                var options = new DbContextOptionsBuilder<EShopContext>()
                    .UseSqlite(connection)
                    .Options;
                using (var context = new EShopContext(options))
                {
                    context.Database.EnsureCreated();
                    context.Categories.AddRange(GetCategories());
                    context.Products.AddRange(GetProducts());
                    context.SaveChanges();
                }
                using (var context = new EShopContext(options))
                {
                    var repository = new ProductRepository(context);
                    var result = repository.GetAll();
                    var productWithDependence = result.Single(p => p.ProductId == 1);

                    Assert.Equal(5, result.Count());
                    Assert.Equal(3, productWithDependence.ProductCategories.Count);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public void Update_UpdateProduct_ProductWithProductCategoryCollectionIsUpdated()
        {
            var newproduct = new Product
            {
                ProductId = 1,
                Name = "New",
                Description = "Des21",
                Price = 21,
                ProductCategories = new List<ProductCategory>
                {
                    new ProductCategory { CategoryId = 3 }
                }
            };
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            try
            {
                var options = new DbContextOptionsBuilder<EShopContext>()
                    .UseSqlite(connection)
                    .Options;
                using (var context = new EShopContext(options))
                {
                    context.Database.EnsureCreated();
                    context.Categories.AddRange(GetCategories());
                    context.Products.AddRange(GetProducts());
                    context.SaveChanges();
                }
                using (var context = new EShopContext(options))
                {
                    var repository = new ProductRepository(context);
                    repository.Update(newproduct);
                }

                using (var context = new EShopContext(options))
                {
                    var repository = new ProductRepository(context);
                    var result = repository.GetAll();
                    var productWithDependence = result.Single(p => p.ProductId == newproduct.ProductId);

                    Assert.Equal(newproduct.Name, productWithDependence.Name);
                    Assert.Equal(newproduct.ProductCategories.Count, productWithDependence.ProductCategories.Count);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public void Get_GetProductById_ProductWithProductCategoryCollection()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            try
            {
                var options = new DbContextOptionsBuilder<EShopContext>()
                    .UseSqlite(connection)
                    .Options;
                using (var context = new EShopContext(options))
                {
                    context.Database.EnsureCreated();
                    context.Categories.AddRange(GetCategories());
                    context.Products.AddRange(GetProducts());
                    context.SaveChanges();
                }
                using (var context = new EShopContext(options))
                {
                    var repository = new ProductRepository(context);
                    var result = repository.Get(1);

                    Assert.Equal(3, result.ProductCategories.Count());
                    Assert.Equal("P21", result.Name);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public void Delete_DeleteProductById_ProductWithProductCategoryCollectionIsDeleted()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            try
            {
                var options = new DbContextOptionsBuilder<EShopContext>()
                    .UseSqlite(connection)
                    .Options;
                using (var context = new EShopContext(options))
                {
                    context.Database.EnsureCreated();
                    context.Categories.AddRange(GetCategories());
                    context.Products.AddRange(GetProducts());
                    context.SaveChanges();
                }
                using (var context = new EShopContext(options))
                {
                    var repository = new ProductRepository(context);
                    repository.Delete(1);

                    Assert.Equal(0, context.ProductCategories.Where(c => c.ProductId == 1).Count());
                    Assert.Null(context.Products.SingleOrDefault(p => p.ProductId == 1));
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public void Create_CreateProduct_ProductWithProductCategoryCollectionIsCreated()
        {
            var newproduct = new Product
            {
                ProductId = 1,
                Name = "New",
                Description = "Des21",
                Price = 21,
                ProductCategories = new List<ProductCategory>
                {
                    new ProductCategory { CategoryId = 3 }
                }
            };
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            try
            {
                var options = new DbContextOptionsBuilder<EShopContext>()
                    .UseSqlite(connection)
                    .Options;
                using (var context = new EShopContext(options))
                {
                    context.Database.EnsureCreated();
                    context.Categories.AddRange(GetCategories());
                    context.SaveChanges();
                }
                using (var context = new EShopContext(options))
                {
                    var repository = new ProductRepository(context);
                    repository.Create(newproduct);
                }
                using (var context = new EShopContext(options))
                {
                    var isProductExist = context.Products.Any(p => p.ProductId == newproduct.ProductId);
                    var productCategories = context.ProductCategories.Where(pc => pc.ProductId == newproduct.ProductId);

                    Assert.True(isProductExist);
                    Assert.Equal(newproduct.ProductCategories.Count, productCategories.Count());
                }
            }
            finally
            {
                connection.Close();
            }
        }

        private IEnumerable<Product> GetProducts()
        {
            List<Product> products = new List<Product>
            {
                new Product { ProductId = 1, Name = "P21", Description = "Des21", Price = 21, ProductCategories = GetProductCategories() },
                new Product { ProductId = 2, Name = "P22", Description = "Des22", Price = 22 },
                new Product { ProductId = 3, Name = "P23", Description = "Des23", Price = 23 },
                new Product { ProductId = 4, Name = "P24", Description = "Des24", Price = 24 },
                new Product { ProductId = 5, Name = "P25", Description = "Des25", Price = 25 }
            };

            return products;
        }

        private List<Category> GetCategories()
        {
            List<Category> categories = new List<Category>
            {
                new Category { CategoryId = 1, Name = "C1", ParentId = 0, ProductCategories = new List<ProductCategory>()},
                new Category { CategoryId = 2, Name = "C2", ParentId = 2, ProductCategories = new List<ProductCategory>()},
                new Category { CategoryId = 3, Name = "C3", ParentId = 1, ProductCategories = new List<ProductCategory>()},
                new Category { CategoryId = 4, Name = "C4", ParentId = 4, ProductCategories = new List<ProductCategory>()},
                new Category { CategoryId = 5, Name = "C5", ParentId = 1, ProductCategories = new List<ProductCategory>()}
            };

            return categories;
        }

        private List<ProductCategory> GetProductCategories()
        {
            List<ProductCategory> productCategories = new List<ProductCategory>
            {
                new ProductCategory { ProductId = 1, CategoryId = 1},
                new ProductCategory { ProductId = 1, CategoryId = 2},
                new ProductCategory { ProductId = 1, CategoryId = 3},
            };

            return productCategories;
        }
    }
}
