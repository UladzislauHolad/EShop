using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.App.Web.Models.Angular.Catalog
{
    public class CategoryNode
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public bool HasChilds { get; set; }
    }
}
