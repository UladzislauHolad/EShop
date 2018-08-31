using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.App.Web.Models
{
    public class ProductOrderCreateViewModel
    {
        public int ProductOrderId { get; set; }
        public int OrderId { get; set; }
        public int OrderCount { get; set; }
        public int ProductId { get; set; }
    }
}
