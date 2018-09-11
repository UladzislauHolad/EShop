using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Data.Entities
{
    public class PickupPoint
    {
        public int PickupPointId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
