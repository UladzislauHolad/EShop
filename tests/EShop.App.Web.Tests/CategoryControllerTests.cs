using AutoMapper;
using EShop.App.Web.Controllers;
using EShop.App.Web.Models;
using EShop.Data.Entities;
using EShop.Data.Interfaces;
using EShop.Services.DTO;
using EShop.Services.Interfaces;
using EShop.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace EShop.App.Web.Tests
{
    public class CategoryControllerTests
    {
        [Fact]
        public void Index_PaginateCategories_CategoriesPaginated()
        {
            const int page = 2;
            var categories = GetCategories();
            var mock = new Mock<IRepository<Category>>();
            mock.Setup(repo => repo.GetAll()).Returns(categories);
            CategoryController controller = new CategoryController(new CategoryService(mock.Object), GetMapper());
            controller.PageSize = 3;

            var result = (controller.Index(page).ViewData.Model as CategoryListViewModel).Categories.ToArray();

            Assert.True(result.Length == 3);
            Assert.Equal("Same4", result[0].Name);
        }

        [Fact]
        public void Index_SendPageInfo_PageInfoSended()
        {
            var categories = GetCategories();
            var mock = new Mock<IRepository<Category>>();
            mock.Setup(repo => repo.GetAll()).Returns(categories);
            CategoryController controller = new CategoryController(new CategoryService(mock.Object), GetMapper());
            controller.PageSize = 3;

            var result = controller.Index(2).ViewData.Model as CategoryListViewModel;

            PageInfo pageInfo = result.PageInfo;
            Assert.Equal(2, pageInfo.CurrentPage);
            Assert.Equal(3, pageInfo.ItemsPerPage);
            Assert.Equal(6, pageInfo.TotalItems);
            Assert.Equal(2, pageInfo.TotalPages);
        }

        [Fact]
        public void Edit_ReturnCategoryiewModel_CategoryViewModelReturned()
        {
            const int testId = 1;
            var categories = GetCategories();
            var mock = new Mock<IRepository<Category>>();
            mock.Setup(repo => repo.Get(testId)).Returns(new Category { CategoryId = 1, Name = "Same1", ParentId = 0 });
            CategoryController controller = new CategoryController(new CategoryService(mock.Object), GetMapper());

            var result = controller.Edit(testId).ViewData.Model as CategoryViewModel;

            Assert.Equal("Same1", result.Name);
        }

        [Fact]
        public void Edit_UpdateCategory_CategoryUpdated()
        {
            Category testCategory = new Category { CategoryId = 1, Name = "Same1", ParentId = 0 };
            var categories = GetCategories();
            var mock = new Mock<IRepository<Category>>();
            mock.Setup(repo => repo.Update(testCategory));
            var mapper = GetMapper();
            CategoryController controller = new CategoryController(new CategoryService(mock.Object), mapper);
            
            controller.Edit(mapper.Map<CategoryViewModel>(testCategory));

            mock.Verify(m => m.Update(It.Is<Category>(c => c.CategoryId == 1)), Times.Once());
        }

        [Fact]
        public void Delete_DeleteCategory_CategoryDeleted()
        {
            const int testId = 1;
            var mock = new Mock<IRepository<Category>>();
            mock.Setup(m => m.Delete(testId));
            var service = new CategoryService(mock.Object);
            var mapper = GetMapper();
            CategoryController controller = new CategoryController(service, mapper);

            controller.Delete(testId);

            mock.Verify(m => m.Delete(testId), Times.Once());
        }

        [Fact]
        public void Create_ReturnView_ViewWithModelIsReturned()
        {
            var mock = new Mock<IRepository<Category>>();
            var service = new CategoryService(mock.Object);
            var mapper = GetMapper();
            CategoryController controller = new CategoryController(service, mapper);

            var result = (controller.Create() as ViewResult).ViewData.Model is CategoryViewModel;

            Assert.True(result);
        }

        [Fact]
        public void Create_InvokeWithValidCategoryViewModel_RedirectedToIndex()
        {
            CategoryViewModel testCategory = new CategoryViewModel { CategoryId = 1, Name = "Same1", ParentId = 0 };
            var mock = new Mock<IRepository<Category>>();
            var service = new CategoryService(mock.Object);
            var mapper = GetMapper();
            CategoryController controller = new CategoryController(service, mapper);

            var result = controller.Create(testCategory);

            Assert.True(result is RedirectToActionResult);
        }

        [Fact]
        public void Childs_Invoke_JsonReturned()
        {
            const int testId = 1;
            var mock = new Mock<ICategoryService>();
            CategoryController controller = new CategoryController(mock.Object, GetMapper());

            var result = controller.Childs(testId);

            Assert.True(result is JsonResult);
        }

        [Fact]
        public void CategorySelect_Invoke_ReturnPartialView()
        {
            var mock = new Mock<ICategoryService>();
            CategoryController controller = new CategoryController(mock.Object, GetMapper());

            var result = controller.CategorySelect();

            Assert.True(result is PartialViewResult);
        }

        [Fact]
        public void ParentCategorySelect_Invoke_ReturnPartialView()
        {
            var mock = new Mock<ICategoryService>();
            CategoryController controller = new CategoryController(mock.Object, GetMapper());

            var result = controller.ParentCategorySelect();

            Assert.True(result is PartialViewResult);
        }

        private IEnumerable<Category> GetCategories()
        {
            List<Category> categories = new List<Category>
            {
                new Category { CategoryId = 1, Name = "Same1", ParentId = 0 },
                new Category { CategoryId = 2, Name = "Same2", ParentId = 1 },
                new Category { CategoryId = 3, Name = "Same3", ParentId = 1 },
                new Category { CategoryId = 4, Name = "Same4", ParentId = 1 },
                new Category { CategoryId = 5, Name = "Same5", ParentId = 1 },
                new Category { CategoryId = 6, Name = "Same6", ParentId = 1 }
            };

            return categories;
        }

        private IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Category, CategoryDTO>();
                cfg.CreateMap<CategoryDTO, Category>();
            });

            return new Mapper(config);
        }
    }
}
