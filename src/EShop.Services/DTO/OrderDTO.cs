using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Services.DTO
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public virtual ICollection<ProductDTO> Products { get; set; }
    }
}
