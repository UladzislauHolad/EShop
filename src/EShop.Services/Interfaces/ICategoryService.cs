using EShop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Services.Interfaces
{
    interface ICategoryRepository
    {
        IEnumerable<Category> GetCategories();
    }
}
