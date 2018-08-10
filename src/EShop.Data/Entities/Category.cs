using System.Collections.Generic;

namespace EShop.Data.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }

        public virtual ICollection<ProductCategory> Products { get; set; }
    }
}
