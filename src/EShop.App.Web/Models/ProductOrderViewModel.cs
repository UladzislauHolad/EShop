using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.App.Web.Models
{
    public class ProductOrderViewModel
    {
        public int ProductOrderId { get; set; }
        public int Count { get; set; }
        public ProductViewModel Product { get; set; }
    }
}
