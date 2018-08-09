﻿using EShop.Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Services.Interfaces
{
    public interface IProductService
    {
        ProductDTO GetProduct(int? id);
        IEnumerable<ProductDTO> GetProducts();
        void Add(ProductDTO product);
    }
}
