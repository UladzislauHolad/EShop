using EShop.Services.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace EShop.Services.Tests
{
    class ProductDTOComparer : IEqualityComparer<ProductDTO>
    {
        public bool Equals(ProductDTO x, ProductDTO y)
        {
            if (x == null && y == null)
                return true;
            else if (y == null | x == null)
                return false;
            else if (x.ProductId == y.ProductId 
                && x.Name == y.Name
                && x.Price == y.Price
                && x.Description == y.Description)
                return true;
            else
                return false;

        }

        public int GetHashCode(ProductDTO obj)
        {
            return obj.GetHashCode();
        }
    }
}
