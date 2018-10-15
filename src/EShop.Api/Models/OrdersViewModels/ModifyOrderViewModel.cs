using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Api.Models.OrdersViewModels
{
    public class ModifyOrderViewModel
    {
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }
        public int CustomerId { get; set; }
        public CustomerViewModel Customer { get; set; }
        public int PaymentMethodId { get; set; }
        public int DeliveryMethodId { get; set; }
        public int? PickupPointId { get; set; }
    }
}
