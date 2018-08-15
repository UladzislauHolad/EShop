using Autofac;
using EShop.Data.EF.Interfaces;
using EShop.Data.Entities;
using EShop.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace EShop.Data.Tests
{
    public class CategoryRepositoryTests
    {
        private Mock<IDbContext> contextMock;
       
        [Fact]
        public void GetAll_Invoke_CategoriesReturned()
        {
            contextMock = new Mock<IDbContext>();
            contextMock = contextMock.SetupDbSet<Category>(GetCategories());
            var repository = new CategoryRepository(contextMock.Object);

            var result = repository.GetAll();

            Assert.Equal("C3", result.ElementAt(2).Name);
        }

        private List<Category> GetCategories()
        {
            List<Category> categories = new List<Category>
            {
                new Category { CategoryId = 1, Name = "C1", ParentId = 1},
                new Category { CategoryId = 2, Name = "C2", ParentId = 2},
                new Category { CategoryId = 3, Name = "C3", ParentId = 3},
                new Category { CategoryId = 4, Name = "C4", ParentId = 4},
                new Category { CategoryId = 5, Name = "C5", ParentId = 5}
            };

            return categories;
        }
    }
}
