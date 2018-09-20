using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Services.DTO
{
    public class CustomerDTO : IEquatable<CustomerDTO>
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        //public virtual ICollection<OrderDTO> Orders { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as CustomerDTO);
        }

        public bool Equals(CustomerDTO other)
        {
            return other != null &&
                   FirstName == other.FirstName &&
                   LastName == other.LastName &&
                   Patronymic == other.Patronymic &&
                   Phone == other.Phone &&
                   Address == other.Address;
        }

        public override int GetHashCode()
        {
            var hashCode = 240610697;
            hashCode = hashCode * -1521134295 + CustomerId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FirstName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LastName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Patronymic);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Phone);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Address);
            //hashCode = hashCode * -1521134295 + EqualityComparer<ICollection<OrderDTO>>.Default.GetHashCode(Orders);
            return hashCode;
        }

        public static bool operator ==(CustomerDTO dTO1, CustomerDTO dTO2)
        {
            return EqualityComparer<CustomerDTO>.Default.Equals(dTO1, dTO2);
        }

        public static bool operator !=(CustomerDTO dTO1, CustomerDTO dTO2)
        {
            return !(dTO1 == dTO2);
        }
    }
}
