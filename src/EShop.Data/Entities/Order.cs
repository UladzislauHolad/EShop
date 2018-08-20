using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Data.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public virtual ICollection<ProductOrder> ProductOrders { get; set; }
    }
}
