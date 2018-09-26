using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.App.Web.Models
{
    public class ProductOrderListViewModel
    {
        public int OrderId { get; set; }
        public string Status { get; set; } = "New";
        public List<ProductOrderViewModel> ProductOrders { get; set; }
    }
}
