using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Data.Entities
{
    public class DeliveryMethod
    {
        public int DeliveryMethodId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
