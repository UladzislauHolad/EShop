using AutoMapper;
using EShop.Data.Entities;
using EShop.Data.Interfaces;
using EShop.Services.DTO;
using EShop.Services.Interfaces;
using System.Collections.Generic;

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
