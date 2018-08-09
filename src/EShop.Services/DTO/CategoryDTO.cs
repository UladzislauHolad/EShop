using EShop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Services.DTO
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }

        public ICollection<ProductCategoryDTO> ProductCategoryDTOs { get; set; }
    }
}
