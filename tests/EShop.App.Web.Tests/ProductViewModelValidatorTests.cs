using EShop.App.Web.Models;
using EShop.App.Web.Models.ModelValidators;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EShop.App.Web.Tests
{
    public class ProductViewModelValidatorTests
    {
        [Fact]
        public void ProductViewModelValidator_ValidateValidModel_ResultTrue()
        {
            var validator = new ProductViewModelValidator();
            var validProduct = new ProductViewModel { Name = "name", Price = 123, Description = "desc", CategoriesId = new List<int>() };

            var result = validator.Validate(validProduct).IsValid;

            Assert.True(result);
        }
    }
}
