using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Services.DTO
{
    public class OrderStatusChangeDTO
    {
        public int OrderStatusChangeId { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }

        public int OrderId { get; set; }
        public OrderDTO Order { get; set; }
    }
}
