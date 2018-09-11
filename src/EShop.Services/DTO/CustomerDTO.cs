using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Services.DTO
{
    public class CustomerDTO
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public virtual ICollection<OrderDTO> Orders { get; set; }
    }
}
