﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.App.Web.Models
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public bool IsConfirmed { get; set; }
        public List<ProductOrderViewModel> ProductOrders { get; set; }
    }
}
