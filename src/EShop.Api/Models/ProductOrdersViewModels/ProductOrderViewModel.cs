using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Api.Models.ProductOrdersViewModels
{
    public class ProductOrderViewModel
    {
        public int ProductOrderId { get; set; }
        public int OrderCount { get; set; }
        public int MaxCount { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
