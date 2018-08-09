using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Services.DTO
{
    public class ProductCategoryDTO
    {
        public int ProductId { get; set; }
        public ProductDTO Product { get; set; }

        public int CategoryId { get; set; }
        public CategoryDTO Category { get; set; }
    }
}
