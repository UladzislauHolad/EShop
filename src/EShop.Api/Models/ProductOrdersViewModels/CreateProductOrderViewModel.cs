using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Api.Models.ProductOrdersViewModels
{
    public class CreateProductOrderViewModel
    {
        public int ProductOrderId { get; set; }
        public int OrderId { get; set; }
        public int OrderCount { get; set; }
        public int ProductId { get; set; }
    }
}
