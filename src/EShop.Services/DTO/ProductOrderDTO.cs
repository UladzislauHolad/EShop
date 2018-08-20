using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Services.DTO
{
    public class ProductOrderDTO
    {
        public int ProductOrderId { get; set; }
        public int Count { get; set; }
        public ProductDTO Product { get; set; }
    }
}
