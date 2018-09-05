using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Data.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public bool IsConfirmed { get; set; }
        public virtual ICollection<ProductOrder> ProductOrders { get; set; }

        public Customer Customer { get; set; }

        public PaymentMethod PaymentMethod { get; set; }
    }
}
