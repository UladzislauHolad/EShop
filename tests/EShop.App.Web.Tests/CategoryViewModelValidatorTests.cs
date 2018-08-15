using EShop.App.Web.Models;
using EShop.App.Web.Models.ModelValidators;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EShop.App.Web.Tests
{
    public class CategoryViewModelValidatorTests
    {
        [Fact]
        public void CategoryViewModelValidator_ValidateValidModel_ResultTrue()
        {
            CategoryViewModel category = new CategoryViewModel { CategoryId = 1, Name = "name" };
            CategoryViewModelValidator rules = new CategoryViewModelValidator();

            Assert.True(rules.Validate(category).IsValid);
        }
    }
}
