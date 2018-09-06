using EShop.Data.Entities;
using EShop.Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Services.Interfaces
{
    public interface IPaymentMethodService
    {
        IEnumerable<PaymentMethodDTO> GetPaymentMethods();
    }
}
