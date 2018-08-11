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
        public void Index_GeneratePageLinks_PageLinksGenerated()
        {
            var urlHelper = new Mock<IUrlHelper>();
            urlHelper.SetupSequence(x => x.Action(It.IsAny<UrlActionContext>()))
                .Returns("Test/Page1")
                .Returns("Test/Page2")
                .Returns("Test/Page3");
            var urlHelperFactory = new Mock<IUrlHelperFactory>();
            urlHelperFactory.Setup(f => f.GetUrlHelper(It.IsAny<ActionContext>()))
                .Returns(urlHelper.Object);
            PageLinkTagHelper helper = new PageLinkTagHelper(urlHelperFactory.Object)
            {
                PageModel = new PageInfo
                {
                    CurrentPage = 2,
                    TotalItems = 28,
                    ItemsPerPage = 10
                },
                PageAction = "Test"
            };
            TagHelperContext ctx = new TagHelperContext(
                new TagHelperAttributeList(),
                new Dictionary<object, object>(), "");
            var content = new Mock<TagHelperContent>();
            TagHelperOutput output = new TagHelperOutput(
                "div",
                new TagHelperAttributeList(),
                (cache, encoder) => Task.FromResult(content.Object));

            helper.Process(ctx, output);

            Assert.Equal(@"<a href=""Test/Page1"">1</a>"
                + @"<a href=""Test/Page2"">2</a>"
                + @"<a href=""Test/Page3"">3</a>", output.Content.GetContent());
        }

        [Fact]
        public void Index_SendPageInfo_PageInfoSended()
        {
            ProductController controller = new ProductController(GetService(GetProducts()), GetMapper());

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
            Product testProduct = new Product
            {
                ProductId = 1,
                Name = "dasda",
                Price = 321,
                Description = "dsaafa",
                ProductCategories = null

            };
            var mock = new Mock<IRepository<Product>>();
            mock.Setup(m => m.Update(testProduct));
            var service = new ProductService(mock.Object);
            var mapper = GetMapper();
            ProductController controller = new ProductController(service, mapper);

            controller.Edit(mapper.Map<Product, ProductViewModel>(testProduct));

            mock.Verify(m => m.Update(It.Is<Product>(p => p.ProductId == 1)), Times.Once());
        }

        [Fact]
        public void Delete_DeleteProduct_ProductDeleted()
        {
            const int testId = 1;
            var mock = new Mock<IRepository<Product>>();
            mock.Setup(m => m.Delete(testId));
            var service = new ProductService(mock.Object);
            var mapper = GetMapper();
            ProductController controller = new ProductController(service, mapper);

            controller.Delete(testId);

            mock.Verify(m => m.Delete(testId), Times.Once());
        }

        private IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ProductDTO, ProductViewModel>();
                cfg.CreateMap<ProductViewModel, ProductDTO>();

                cfg.CreateMap<Product, ProductViewModel>()
                    .ForMember(dest => dest.Categories,
                        opt => opt.MapFrom(src => src.ProductCategories));
            });
            return new Mapper(config);
        }

        private IProductService GetService(IEnumerable<Product> products)
        {
            var mock = new Mock<IRepository<Product>>();
            mock.Setup(repo => repo.GetAll()).Returns(products);
            return new ProductService(mock.Object);
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
