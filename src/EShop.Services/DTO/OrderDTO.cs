using EShop.Services.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Services.DTO
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; } = "New";
        public string Comment { get; set; }

        public ICollection<ProductOrderDTO> ProductOrders { get; set; }
        public CustomerDTO Customer { get; set; }
        public int PaymentMethodId { get; set; }
        public PaymentMethodDTO PaymentMethod { get; set; }
        public int? DeliveryMethodId { get; set; }
        public DeliveryMethodDTO DeliveryMethod { get; set; }
        public Commands Command { get; set; }
        public ICollection<OrderStatusChangeDTO> OrderStatusChanges { get; set; }
    }
}
