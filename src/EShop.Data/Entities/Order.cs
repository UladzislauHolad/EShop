using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Data.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; } = "New";
        public virtual ICollection<ProductOrder> ProductOrders { get; set; }

        public Customer Customer { get; set; }

        public int PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public int? DeliveryMethodId { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
    }
}
