using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.App.Web.Models.Angular.Catalog
{
    public class CategoryNestedNodeViewModel
    {
        public CategoryViewModel Category { get; set; }
        public List<CategoryNestedNodeViewModel> Childs { get; set; }
    }
}
