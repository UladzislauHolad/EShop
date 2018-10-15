using EShop.Api.Models.CategoriesViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Api.Models.ProductsViewModels
{
    public class UpdateProductViewModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }

        public List<CategoryViewModel> Categories { get; set; }
    }
}
