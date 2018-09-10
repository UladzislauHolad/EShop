using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Data.Entities
{
    public class OrderStatusChange
    {
        public int OrderStatusChangeId { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
