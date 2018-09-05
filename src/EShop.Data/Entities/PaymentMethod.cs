using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Data.Entities
{
    public class PaymentMethod
    {
        public int PaymentMethodId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
