using System;
using System.Collections.Generic;

namespace EShop.Services.DTO
{
    public class CategoryDTO : IEquatable<CategoryDTO>
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }

        public bool Equals(CategoryDTO other)
        {
            return other != null &&
                   CategoryId == other.CategoryId &&
                   Name == other.Name &&
                   ParentId == other.ParentId;
        }
    }
}
