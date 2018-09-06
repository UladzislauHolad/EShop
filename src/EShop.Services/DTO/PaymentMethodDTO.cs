using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Services.DTO
{
    public class PaymentMethodDTO
    {
        public int PaymentMethodId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<OrderDTO> Orders { get; set; }
    }
}
