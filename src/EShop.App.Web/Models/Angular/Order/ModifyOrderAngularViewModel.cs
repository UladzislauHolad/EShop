using EShop.App.Web.Models.DeliveryMethodViewModels;
using EShop.App.Web.Models.PaymentMethodViewModels;
using EShop.App.Web.Models.PickupPointViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.App.Web.Models.Angular
{
    public class ModifyOrderAngularViewModel
    {
        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }
        public int CustomerId { get; set; }
        public CustomerViewModel Customer { get; set; }
        public int PaymentMethodId { get; set; }
        public int DeliveryMethodId { get; set; }
        public int? PickupPointId { get; set; }
    }
}
