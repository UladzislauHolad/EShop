using EShop.Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EShop.Services.Tests
{
    public class CategoryDTOTests
    {
        [Fact]
        public void Equals_ComparingSameCategoryDTOs_CategoryDTOsIsEqual()
        {
            CategoryDTO c1 = new CategoryDTO { CategoryId = 1, Name = "Same", ParentId = 1 };
            CategoryDTO c2 = new CategoryDTO { CategoryId = 1, Name = "Same", ParentId = 1 };

            Assert.Equal(c1, c2);
        }

        [Fact]
        public void Equals_ComparingCategoryDTOs_CategoryDTOsIsNotEqual()
        {
            CategoryDTO c1 = new CategoryDTO { CategoryId = 1, Name = "Same", ParentId = 1 };
            CategoryDTO c2 = new CategoryDTO { CategoryId = 2, Name = "Same", ParentId = 1 };

            Assert.NotEqual(c1, c2);
        }
    }
}
