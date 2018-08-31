﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Data.Entities
{
    public class ProductOrder
    {
        public int ProductOrderId { get; set; }
        public int OrderId { get; set; }
        public int OrderCount { get; set; }

        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        public Product Product { get; set; }
    }
}
