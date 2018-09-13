using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.App.Web.Models.OrderViewModels
{
    public class OrderListViewModel
    {
        public PaginatedList<OrderViewModel> Orders { get; set; }

        public string IdSort { get; set; }
        public string CustomerSort { get; set; }
        public string StatusSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentOrderFilter { get; set; }
        public string CurrentSort { get; set; }
        public string CurrentSearchString { get; set; }
        public DateTime CurrentFrom { get; set; }
        public DateTime CurrentTo { get; set; }
    }
}
