using EShop.App.Web.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EShop.App.Web.Tests
{
    public class ProductViewModelTests
    {
        [Fact]
        public void SetCategoriesId_SetList_ListSeted()
        {
            ProductViewModel p = new ProductViewModel();

            p.CategoriesId = new List<int>();

            Assert.NotNull(p.CategoriesId);
        }
    }
}
