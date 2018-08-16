using EShop.Data.EF;
using EShop.Data.EF.Interfaces;
using EShop.Data.Entities;
using EShop.Data.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EShop.Data.Tests
{
    public class CategoryRepositoryTests
    {
        [Fact]
        public void GetAll_Invoke_CategoriesReturned()
        {
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
                    context.SaveChanges();
                }

                using (var context = new EShopContext(options))
                {
                    var repository = new CategoryRepository(context);
                    var result = repository.GetAll();

                    Assert.Equal(5, result.Count());
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public void Find_FindCategoryById_CategoryIsFound()
        {
            var category = new Category { CategoryId = 1, Name = "C1", ParentId = 1, ProductCategories = new List<ProductCategory>() };

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
                    context.SaveChanges();
                }

                using (var context = new EShopContext(options))
                {
                    var repository = new CategoryRepository(context);
                    var result = repository.Find(c => c.CategoryId == category.CategoryId);

                    Assert.Contains(result, c => c.CategoryId == category.CategoryId);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public void Update_UpdateCategory_CategoryIsUpdated()
        {
            var category = new Category { CategoryId = 1, Name = "C7", ParentId = 1, ProductCategories = new List<ProductCategory>() };

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
                    context.SaveChanges();
                }

                using (var context = new EShopContext(options))
                {
                    var repository = new CategoryRepository(context);
                    repository.Update(category);
                }

                using (var context = new EShopContext(options))
                {
                    var repository = new CategoryRepository(context);
                    var result = repository.Get(category.CategoryId);

                    Assert.Equal("C7", result.Name);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public void Delete_DeleteCategory_CategoryIsDeletedWithChilds()
        {
            var parent = new Category { CategoryId = 1, Name = "C1", ParentId = 0, ProductCategories = new List<ProductCategory>() };
            var child1 = new Category { CategoryId = 3, Name = "C3", ParentId = 1, ProductCategories = new List<ProductCategory>() };
            var child2 = new Category { CategoryId = 5, Name = "C5", ParentId = 1, ProductCategories = new List<ProductCategory>() };
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
                    context.SaveChanges();
                }

                using (var context = new EShopContext(options))
                {
                    var repository = new CategoryRepository(context);
                    repository.Delete(parent.CategoryId);
                }

                using (var context = new EShopContext(options))
                {
                    var repository = new CategoryRepository(context);
                    var collection = repository.GetAll();

                    Assert.DoesNotContain(parent, collection);
                    Assert.DoesNotContain(child1, collection);
                    Assert.DoesNotContain(child2, collection);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public void Create_CreateCategory_CategoryIsCreated()
        {
            var category = new Category { CategoryId = 1, Name = "C1", ParentId = 1, ProductCategories = new List<ProductCategory>() };

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
                    var repository = new CategoryRepository(context);
                    repository.Create(category);
                }

                using (var context = new EShopContext(options))
                {
                    var repository = new CategoryRepository(context);
                    var result = repository.Get(category.CategoryId);

                    Assert.Equal("C1", result.Name);
                }
            }
            finally
            {
                connection.Close();
            }
        }


        //[Fact]
        //public void Delete_InvokeWithId_CategoryIsDeleted()
        //{
        //    var category = new Category { CategoryId = 1, Name = "C1", ParentId = 1 };

        //    repository.Delete(1);

        //    contextMock.Verify(c => c.Remove(category), Times.Once);
        //}

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
    }
}
