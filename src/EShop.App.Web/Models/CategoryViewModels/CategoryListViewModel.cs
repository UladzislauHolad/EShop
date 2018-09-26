using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.App.Web.Models
{
    public class CategoryListViewModel
    {
        public IEnumerable<CategoryViewModel> Categories { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
