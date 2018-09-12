using EShop.App.Web.Models.DeliveryMethodViewModels;
using EShop.App.Web.Models.OrderViewModels;
using EShop.App.Web.Models.PaymentMethodViewModels;
using EShop.App.Web.Models.PickupPointViewModels;
using EShop.Services.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.App.Web.Models
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; } = "OnCreating";
        public string Comment { get; set; }

        public List<ProductOrderViewModel> ProductOrders { get; set; }

        public int CustomerId { get; set; }
        public CustomerViewModel Customer { get; set; }

        public int PaymentMethodId { get; set; }
        public PaymentMethodViewModel PaymentMethod { get; set; }

        public int DeliveryMethodId { get; set; }
        public DeliveryMethodViewModel DeliveryMethod { get; set; }

        public int PickupPointId { get; set; }
        public PickupPointViewModel PickupPoint { get; set; }

        public Commands Command { get; set; }
        public ButtonConfiguration ButtonConfiguration { get; set; }
        public FormConfiguration FormConfiguration { get; set; }
    }
}
