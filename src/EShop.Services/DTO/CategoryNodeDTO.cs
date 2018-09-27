using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Services.DTO
{
    public class CategoryNodeDTO
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public bool HasChilds { get; set; }
    }
}
