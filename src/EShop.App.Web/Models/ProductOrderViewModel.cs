using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.App.Web.Models
{
    public class ProductOrderViewModel
    {
        public int ProductOrderId { get; set; }
        public int OrderId { get; set; }
        public int OrderCount { get; set; }

        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsNotAvailable { get; set; }

        public ProductViewModel Product { get; set; }
    }
}
