using EShop.Data.Entities;
using EShop.Data.Interfaces;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using EShop.Services.Services;
using AutoMapper;
using EShop.Services.DTO;
using System;

namespace EShop.Services.Tests
{
    public class CategoryServiceTests
    {
        [Fact]
        public void GetCategories_CheckCountOfCategoriesInRepositoryAndFromService_CountsOfCategoriesAreMatch()
        {
            var categories = GetCategories();
            int expected = categories.Count();
            var mock = new Mock<IRepository<Category>>();
            mock.Setup(repo => repo.GetAll()).Returns(categories);
            CategoryService service = new CategoryService(mock.Object);

            int result = service.GetCategories().Count();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetCategories_MappingCategoryToCategoryDTO_CategoryToCategoryDTOMapped()
        {
            var categories = GetCategories();
            var mock = new Mock<IRepository<Category>>();
            mock.Setup(repo => repo.GetAll()).Returns(categories);
            CategoryService service = new CategoryService(mock.Object);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Category, CategoryDTO>()).CreateMapper();
            var expected = mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(categories);

            var result = service.GetCategories();

            Assert.True(expected.SequenceEqual(result));
        }

        [Fact]
        public void GetCategories_IsResultHaveTypeIEnumerableCategoryDTO_ResultIsIEnumerableCategoryDTO()
        {
            var categories = GetCategories();
            var mock = new Mock<IRepository<Category>>();
            mock.Setup(repo => repo.GetAll()).Returns(categories);
            CategoryService service = new CategoryService(mock.Object);

            var result = service.GetCategories();

            Assert.IsAssignableFrom<IEnumerable<CategoryDTO>>(result);
        }

        [Fact]
        public void Create_CreatingProductInRepository_ProductCreated()
        {
            var category = new Category { CategoryId = 1, Name = "P21", ParentId = 1 };
            var mock = new Mock<IRepository<Category>>();
            mock.Setup(m => m.Create(category));
            var service = new CategoryService(mock.Object);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Category, CategoryDTO>()).CreateMapper();

            service.Create(mapper.Map<Category, CategoryDTO>(category));

            mock.Verify(m => m.Create(It.Is<Category>(c => c.Name == category.Name)), Times.Once());
        }

        [Fact]
        public void Delete_DeletingProductFromRepository_ProductDeleted()
        {
            int id = 2;
            var mock = new Mock<IRepository<Category>>();
            mock.Setup(m => m.Delete(id));
            var service = new CategoryService(mock.Object);

            service.Delete(id);

            mock.Verify(m => m.Delete(id), Times.Once());
        }

        [Fact]
        public void Update_UpdatingProductInRepository_ProductUpdated()
        {
            var category = new Category { CategoryId = 1, Name = "P21", ParentId = 1 };
            var mock = new Mock<IRepository<Category>>();
            mock.Setup(m => m.Update(category));
            var service = new CategoryService(mock.Object);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Category, CategoryDTO>()).CreateMapper();

            service.Update(mapper.Map<Category, CategoryDTO>(category));

            mock.Verify(m => m.Update(It.Is<Category>(c => c.Name == category.Name)), Times.Once());
        }



        [Fact]
        public void GetChildCategories_Invoke_ReturnedCollectionOfCategoryDTO()
        {
            const int id = 1;
            var mock = new Mock<IRepository<Category>>();
            mock.Setup(m => m.Find(It.IsAny<Func<Category, bool>>())).Returns(GetCategories());
            var service = new CategoryService(mock.Object);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Category, CategoryDTO>()).CreateMapper();

            var result = service.GetChildCategories(id).ToArray();

            Assert.Equal("C1", result[0].Name);
        }

        private IEnumerable<Category> GetCategories()
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
