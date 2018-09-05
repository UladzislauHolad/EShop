using EShop.App.Web.Controllers;
using EShop.Data.Entities;
using EShop.Data.Interfaces;
using EShop.Services.Interfaces;
using EShop.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using Xunit;
using AutoMapper;
using EShop.App.Web.Models;
using EShop.Services.DTO;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Routing;
using EShop.App.Web.Infrastructure;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace EShop.App.Web.Tests
{
    public class ProductControllerTests
    {
        [Fact]
        public void Index_GetNotNullViewResult_GotNotNullViewResult()
        {
            ProductController controller = new ProductController(GetService(GetProducts()), GetMapper());

            ViewResult result = controller.Index() as ViewResult;

            Assert.NotNull(result);
        }

        [Fact]
        public void Index_PaginateProducts_ProductsPaginated()
        {
            const int page = 2;
            ProductController controller = new ProductController(GetService(GetProducts()), GetMapper());
            controller.PageSize = 3;

            IEnumerable<ProductViewModel> prodArray = (controller.Index(page).ViewData.Model as ProductListViewModel).Products;
            ProductViewModel[] result = prodArray.ToArray();

            Assert.True(result.Length == 2);
            Assert.Equal("P24", result[0].Name);
        }

        [Fact]
        public void Index_SendPageInfo_PageInfoSended()
        {
            ProductController controller = new ProductController(GetService(GetProducts()), GetMapper());
            controller.PageSize = 3;

            ProductListViewModel result = controller.Index(2).ViewData.Model as ProductListViewModel;

            PageInfo pageInfo = result.PageInfo;
            Assert.Equal(2, pageInfo.CurrentPage);
            Assert.Equal(3, pageInfo.ItemsPerPage);
            Assert.Equal(5, pageInfo.TotalItems);
            Assert.Equal(2, pageInfo.TotalPages);
        }

        [Fact]
        public void Edit_ReturnProductViewModel_ProductViewModelReturned()
        {
            const int testId = 1;
            var mock = new Mock<IRepository<Product>>();
            mock.Setup(m => m.Get(testId)).Returns(new Product { ProductId = 1, Name = "P21", Description = "Des21", Price = 21 });
            var service = new ProductService(mock.Object);
            var mapper = GetMapper();
            ProductController controller = new ProductController(service, mapper);

            var result = controller.Edit(testId).ViewData.Model as ProductViewModel;

            Assert.Equal("P21", result.Name);
        }

        [Fact]
        public void Edit_UpdateProduct_ProductUpdated()
        {
            ProductViewModel productVM = new ProductViewModel
            {
                ProductId = 1,
                Name = "name",
                Price = 321,
                Description = "desc",
                CategoriesId = new List<int> { 1, 2, 3 }
            };
            var mapper = GetMapper();
            var product = mapper.Map<Product>(mapper.Map<ProductDTO>(productVM));
            var mock = new Mock<IRepository<Product>>();
            mock.Setup(m => m.Update(product));
            var service = new ProductService(mock.Object);
            ProductController controller = new ProductController(service, mapper);

            controller.Edit(productVM);

            mock.Verify(m => m.Update(It.Is<Product>(p => p.ProductId == 1)), Times.Once());
        }

        [Fact]
        public void Edit_UpdateNotValidModel_ReturnView()
        {
            var mock = new Mock<IProductService>();
            ProductController controller = new ProductController(mock.Object, GetMapper());
            var invalidProduct = new ProductViewModel();
            controller.ModelState.AddModelError("", "");

            var result = controller.Edit(invalidProduct);

            Assert.True(result is ViewResult);
        }

        [Fact]
        public void Delete_DeleteProduct_ProductDeleted()
        {
            const int testId = 1;
            var mock = new Mock<IProductService>();
            mock.Setup(m => m.Delete(testId));
            var mapper = GetMapper();
            ProductController controller = new ProductController(mock.Object, mapper);

            controller.Delete(testId);

            mock.Verify(m => m.Delete(testId), Times.Once());
        }

        [Fact]
        public void AddProduct_ReturnViewWithProductViewModel_ViewReturnedWithProductViewModel()
        {
            var mock = new Mock<IProductService>();            
            var mapper = GetMapper();
            ProductController controller = new ProductController(mock.Object, mapper);

            var result = controller.AddProduct().ViewData.Model;

            Assert.NotNull(result);
            Assert.True(result is ProductViewModel);
        }

        [Fact]
        public void AddProduct_AddValidModel_RedirectToIndex()
        {
            var mock = new Mock<IProductService>();
            var mapper = GetMapper();
            ProductController controller = new ProductController(mock.Object, mapper);
            var validProduct = new ProductViewModel();
            validProduct.Name = "name";
            validProduct.Price = 1231;
            validProduct.Description = "desc";
            validProduct.CategoriesId = new List<int> { 1, 2, 3 };

            var result = controller.AddProduct(validProduct) as RedirectToActionResult;

            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public void Products_GetProductsById_ProductsAreGot()
        {
            const int testId = 1;
            var products = GetProducts();
            var mock = new Mock<IRepository<Product>>();
            mock.Setup(m => m.GetAll()).Returns(products);
            var service = new ProductService(mock.Object);
            var mapper = GetMapper();
            ProductController controller = new ProductController(service, mapper);

            var result = controller.Products(testId);

            Assert.NotEmpty(result.ViewData.Model as IEnumerable<ProductViewModel>);
        }

        [Fact]
        public void Categories_SetParameters_ReturnPartialViewWithCollection()
        {
            var mock = new Mock<IProductService>();
            var mapper = GetMapper();
            ProductController controller = new ProductController(mock.Object, mapper);
            var categories = new List<CategoryViewModel> { new CategoryViewModel { CategoryId = 1, Name = "name", ParentId = 2 } };

            var result = controller.Categories(categories);

            Assert.NotEmpty(result.ViewData.Model as IEnumerable<CategoryViewModel>);
        }

        [Fact]
        public void AddProduct_AddNotValidModel_ReturnView()
        {
            var mock = new Mock<IProductService>();
            ProductController controller = new ProductController(mock.Object, GetMapper());
            var invalidProduct = new ProductViewModel();
            controller.ModelState.AddModelError("", "");

            var result = controller.AddProduct(invalidProduct);

            Assert.True(result is ViewResult);
        }

        [Fact]
        public void ProductJson_InvokeWithValidId_JsonResult()
        {
            var mock = new Mock<IProductService>();
            mock.Setup(m => m.GetProducts()).Returns(new List<ProductDTO>());
            ProductController controller = new ProductController(mock.Object, GetMapper());

            var result = controller.ProductJson(1);

            Assert.True(result is JsonResult);
        }

        private IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ProductDTO, ProductViewModel>();
                cfg.CreateMap<ProductViewModel, ProductDTO>();

                cfg.CreateMap<Product, ProductViewModel>()
                    .ForMember(dest => dest.Categories,
                        opt => opt.MapFrom(src => src.ProductCategories));
                cfg.CreateMap<ProductDTO, Product>()
                    .ForMember(dest => dest.ProductCategories,
                        opt => opt.MapFrom(src => src.Categories));
            });
            return new Mapper(config);
        }

        private IProductService GetService(IQueryable<Product> products)
        {
            var mock = new Mock<IRepository<Product>>();
            mock.Setup(repo => repo.GetAll()).Returns(products);
            return new ProductService(mock.Object);
        }


        private IQueryable<Product> GetProducts()
        {
            ProductCategory[] productCategorys = new ProductCategory[]
            {
                new ProductCategory { CategoryId = 1, ProductId = 1 },
                new ProductCategory { CategoryId = 2, ProductId = 2 },
                new ProductCategory { CategoryId = 3, ProductId = 3 },
                new ProductCategory { CategoryId = 4, ProductId = 4 },
                new ProductCategory { CategoryId = 5, ProductId = 5 }
            };

            List<Product> products = new List<Product>
            {
                new Product { ProductId = 1, Name = "P21", Description = "Des21", Price = 21, ProductCategories = productCategorys },
                new Product { ProductId = 2, Name = "P22", Description = "Des22", Price = 22, ProductCategories = productCategorys },
                new Product { ProductId = 3, Name = "P23", Description = "Des23", Price = 23, ProductCategories = productCategorys },
                new Product { ProductId = 4, Name = "P24", Description = "Des24", Price = 24, ProductCategories = productCategorys },
                new Product { ProductId = 5, Name = "P25", Description = "Des25", Price = 25, ProductCategories = productCategorys }
            };

            return products.AsQueryable();
        }
    }
}
