using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.App.Web.Models.OrderViewModels
{
    public class CustomerComparer : IComparer<CustomerViewModel>
    {
        public int Compare(CustomerViewModel x, CustomerViewModel y)
        {
            string xFullName = $"{x.FirstName} {x.LastName} {x.Patronymic}";
            string yFullName = $"{y.FirstName} {y.LastName} {y.Patronymic}";

            return xFullName.CompareTo(yFullName);
        }
    }
}
