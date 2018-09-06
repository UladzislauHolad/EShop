using EShop.App.Web.Models.PaymentMethodViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.App.Web.Models
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; } = "New";
        public List<ProductOrderViewModel> ProductOrders { get; set; }
        public CustomerViewModel Customer { get; set; }
        public int PaymentMethodId { get; set; }
        public PaymentMethodViewModel PaymentMethod { get; set; }
    }
}
