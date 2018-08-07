using EShop.Services.DTO;
using EShop.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using EShop.Data.Interfaces

namespace EShop.Services.Services
{
    class Service : IService
    {
        ProductRepository Database { get; set; }

        public ProductDTO GetProduct(int? id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductDTO> GetProducts()
        {
            throw new NotImplementedException();
        }
    }
}
