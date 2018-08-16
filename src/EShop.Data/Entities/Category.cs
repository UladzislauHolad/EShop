using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EShop.Data.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }

        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
