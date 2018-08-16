using EShop.Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EShop.Services.Tests
{
    public class ProductDTOTests
    {
        [Fact]
        public void Equals_ComparingSameProductDTOs_ProductDTOsIsEqual()
        {
            ProductDTO p1 = new ProductDTO { ProductId = 1, Name = "Same", Description = "Des", Price = 1 };
            ProductDTO p2 = new ProductDTO { ProductId = 1, Name = "Same", Description = "Des", Price = 1 };

            Assert.Equal(p1, p2);
        }

        [Fact]
        public void Equals_ComparingDifferentProductDTOs_ProductDTOsIsNotEqual()
        {
            ProductDTO p1 = new ProductDTO { ProductId = 1, Name = "Same", Description = "Des", Price = 1 };
            ProductDTO p2 = new ProductDTO { ProductId = 2, Name = "Same", Description = "Des", Price = 1 };

            Assert.NotEqual(p1, p2);
        }
    }
}
