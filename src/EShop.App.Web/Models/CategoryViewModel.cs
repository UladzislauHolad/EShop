using System.Collections.Generic;

namespace EShop.App.Web.Models
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
        public ICollection<int> ChildIds { get; set; }
    }
}
