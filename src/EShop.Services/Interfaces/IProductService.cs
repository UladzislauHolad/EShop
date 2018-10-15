using EShop.Services.DTO;
using System.Collections.Generic;

namespace EShop.Services.Interfaces
{
    public interface IProductService
    {
        ProductDTO GetProduct(int? id);
        IEnumerable<ProductDTO> GetProducts();
        void Add(ProductDTO product);
        void Delete(int id);
        void Update(int id, ProductDTO product);
        IEnumerable<ProductDTO> GetProductsByCategoryId(int id);
        List<object> GetCategoriesWithCountOfProducts();
    }
}
