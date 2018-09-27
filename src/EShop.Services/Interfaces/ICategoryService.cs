using EShop.Services.DTO;
using System.Collections.Generic;

namespace EShop.Services.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDTO> GetCategories();
        void Create(CategoryDTO categoryDTO);
        void Delete(int id);
        void Update(CategoryDTO categoryDTO);
        IEnumerable<CategoryDTO> GetChildCategories(int id);
        CategoryDTO GetCategory(int id);
        object GetCategoryNameWithCountOfProducts();
        IEnumerable<CategoryNodeDTO> GetCategoryNodes(int parentId);
    }
}
