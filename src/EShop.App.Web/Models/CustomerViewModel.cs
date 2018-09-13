using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.App.Web.Models
{
    public class CustomerViewModel : IComparable<CustomerViewModel>, IComparer<CustomerViewModel>
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public virtual ICollection<OrderViewModel> Orders { get; set; }

        public int Compare(CustomerViewModel x, CustomerViewModel y)
        {
            return x.CompareTo(y);
        }

        public int CompareTo(CustomerViewModel other)
        {
            string fullName = $"{FirstName} {LastName} {Patronymic}";
            string otherFullName = $"{other.FirstName} {other.LastName} {other.Patronymic}";

            return fullName.CompareTo(otherFullName);
        }
    }
}
