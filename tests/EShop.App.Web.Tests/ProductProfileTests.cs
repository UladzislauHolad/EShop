using AutoMapper;
using EShop.App.Web.Models;
using EShop.Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EShop.App.Web.Tests
{
    public class DomainProfileTests
    {
        [Fact]
        public void DomainProfile_MapToProductDTO_MappedToProductDTO()
        {
            var productViewModel = new ProductViewModel { ProductId = 1, Name = "name", Description = "desc", Price = 123 };
            var mapper = GetMapper();

            var result = mapper.Map<ProductDTO>(productViewModel);

            Assert.True(result is ProductDTO);
        }

        [Fact]
        public void DomainProfile_MapToProductViewModel_MappedToProductViewModel()
        {
            var productDto = new ProductDTO { ProductId = 1, Name = "name", Description = "desc", Price = 123 };
            var mapper = GetMapper();

            var result = mapper.Map<ProductViewModel>(productDto);

            Assert.True(result is ProductViewModel);
        }


        private IRuntimeMapper GetMapper()
        {
            var profile = new ProductProfile();
            MapperConfiguration cfg = new MapperConfiguration(configure => configure.AddProfile(profile));
            return new Mapper(cfg).DefaultContext.Mapper;
        }
    }
}
