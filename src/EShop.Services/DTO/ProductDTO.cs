using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EShop.Services.DTO
{
    public class ProductDTO : IEquatable<ProductDTO>
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as ProductDTO);
        }

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
