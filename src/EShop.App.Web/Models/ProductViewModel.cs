using System.Collections.Generic;

namespace EShop.App.Web.Models
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }

        public List<int> CategoriesId { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
    }
}
