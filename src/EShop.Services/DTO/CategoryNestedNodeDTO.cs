using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Services.DTO
{
    public class CategoryNestedNodeDTO
    {
        public CategoryDTO Category { get; set; }
        public List<CategoryNestedNodeDTO> Childs { get; set; }
    }
}
