using AutoMapper;
using EShop.App.Web.Models;
using EShop.App.Web.Profiles;
using EShop.Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EShop.App.Web.Tests
{
    public class CategoryProfileTests
    {
        [Fact]
        public void CategoryProfile_MapToCategoryDTO_MappedToCategoryDTO()
        {
            var categoryViewModel = new CategoryViewModel { CategoryId = 1, Name = "name", ParentId = 1 };
            var mapper = GetMapper();

            var result = mapper.Map<CategoryDTO>(categoryViewModel);

            Assert.True(result is CategoryDTO);
        }

        [Fact]
        public void CategoryProfile_MapToCategoryViewModel_MappedToCategoryCategoryViewModel()
        {
            var categoryDto = new CategoryDTO { CategoryId = 1, Name = "name", ParentId = 1 };
            var mapper = GetMapper();

            var result = mapper.Map<CategoryViewModel>(categoryDto);

            Assert.True(result is CategoryViewModel);
        }

        private IRuntimeMapper GetMapper()
        {
            var profile = new CategoryProfile();
            MapperConfiguration cfg = new MapperConfiguration(configure => configure.AddProfile(profile));
            return new Mapper(cfg).DefaultContext.Mapper;
        }
    }
}
