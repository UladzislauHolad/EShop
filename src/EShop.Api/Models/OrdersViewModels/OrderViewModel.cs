using EShop.Api.Models.ProductOrdersViewModels;
using EShop.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Api.Models.OrdersViewModels
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }

        public int CustomerId { get; set; }
        public CustomerViewModel Customer { get; set; }

        public int PaymentMethodId { get; set; }
        public PaymentMethodDTO PaymentMethod { get; set; }

        public int DeliveryMethodId { get; set; }
        public DeliveryMethodDTO DeliveryMethod { get; set; }

        public int PickupPointId { get; set; }
        public PickupPointDTO PickupPoint { get; set; }

        public List<ProductOrderViewModel> ProductOrders { get; set; }
    }
}
