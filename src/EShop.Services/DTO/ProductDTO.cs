using System;
using System.Collections.Generic;

namespace EShop.Services.DTO
{
    public class ProductDTO : IEquatable<ProductDTO>
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }

        public ICollection<CategoryDTO> Categories { get; set; }
        public ICollection<ProductOrderDTO> ProductOrders { get; set; }

        public bool Equals(ProductDTO other)
        {
            return other != null &&
                   ProductId == other.ProductId &&
                   Name == other.Name &&
                   Price == other.Price &&
                   Description == other.Description;
        }
    }
}
