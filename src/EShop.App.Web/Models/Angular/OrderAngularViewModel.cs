using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.App.Web.Models.Angular
{
    public class OrderAngularViewModel
    {
        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
    }
}
