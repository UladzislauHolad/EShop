using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Api.Models.OrdersViewModels
{
    public class OrderTableViewModel
    {
        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string DeliveryMethodName { get; set; }
        public string PaymentMethodName { get; set; }
    }
}
