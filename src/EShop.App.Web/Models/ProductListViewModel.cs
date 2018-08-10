using System.Collections.Generic;

namespace EShop.App.Web.Models
{
    public class ProductListViewModel
    {
        public IEnumerable<ProductViewModel> Products { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
