using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.App.Web.Models.OrderViewModels
{
    public class OrderListViewModel
    {
        public IEnumerable<OrderViewModel> Orders { get; set; }

        public string IdSort { get; set; }
        public string CustomerSort { get; set; }
        public string StatusSort { get; set; }
        public string DateSort { get; set; }
        //public string CurrentFilter { get; set; }
        //public string CurrentSort { get; set; }
    }
}
