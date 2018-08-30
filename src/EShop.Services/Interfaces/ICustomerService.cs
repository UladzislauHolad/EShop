using EShop.Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Services.Interfaces
{
    public interface ICustomerService
    {
        IEnumerable<CustomerDTO> GetCustomers();
        void Create(CustomerDTO customerDTO);
        CustomerDTO GetCustomer(int id);
        void Update(CustomerDTO customerDTO);
        void Delete(int id);
    }
}
