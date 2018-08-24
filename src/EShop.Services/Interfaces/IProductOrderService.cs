using EShop.Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Services.Interfaces
{
    public interface IProductOrderService
    {
        void Delete(int id);
        ProductOrderDTO GetProductOrder(int id);
        void Update(ProductOrderDTO productOrderDTO);
    }
}
