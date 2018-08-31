using AutoMapper;
using EShop.Data.Entities;
using EShop.Data.Interfaces;
using EShop.Services.DTO;
using EShop.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace EShop.Services.Services
{
    public class CategoryService : ICategoryService
    {
        IRepository<Category> _repository;

        public CategoryService(IRepository<Category> repository)
        {
            _repository = repository;
        }

        public void Create(CategoryDTO categoryDTO)
        {
            var mapper = GetMapper();

            _repository.Create(mapper.Map<CategoryDTO, Category>(categoryDTO));
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public IEnumerable<CategoryDTO> GetCategories()
        {
            var mapper = GetMapper();

            return mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(_repository.GetAll());
        }

        public CategoryDTO GetCategory(int id)
        {
            var mapper = GetMapper();

            return mapper.Map<CategoryDTO>(_repository.Get(id));
        }

        public object GetCategoryNameWithCountOfProducts()
        {
            var categories = _repository.GetAll();
            var result = categories.Select(c => new { Name = c.Name, Count = c.ProductCategories.Count });

            return result;
        }

        public IEnumerable<CategoryDTO> GetChildCategories(int id)
        {
            var mapper = GetMapper();

            var categories = _repository.Find(c => c.ParentId == id);

            return mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(categories);
        }

        public void Update(CategoryDTO categoryDTO)
        {
            var mapper = GetMapper();

            _repository.Update(mapper.Map<CategoryDTO, Category>(categoryDTO));
        }

        private IMapper GetMapper()
        {
            var mapper = new MapperConfiguration((cfg) =>
                {
                    cfg.CreateMap<Category, CategoryDTO>();
                    cfg.CreateMap<CategoryDTO, Category>();
                }
            ).CreateMapper();

            return mapper;
        }
    }
}
