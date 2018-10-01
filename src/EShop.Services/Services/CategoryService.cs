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
        IMapper _mapper;

        public CategoryService(IRepository<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void Create(CategoryDTO categoryDTO)
        {
            _repository.Create(_mapper.Map<CategoryDTO, Category>(categoryDTO));
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public IEnumerable<CategoryDTO> GetCategories()
        {
            return _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(_repository.GetAll());
        }

        public CategoryDTO GetCategory(int id)
        {
            return _mapper.Map<CategoryDTO>(_repository.Get(id));
        }

        public IEnumerable<CategoryPieChartInfoDTO> GetCategoryNameWithCountOfProducts()
        {
            var categories = _repository.GetAll();
            var result = categories.Select(c => new CategoryPieChartInfoDTO{ Name = c.Name, Value = c.ProductCategories.Count }).ToList();

            return result;
        }

        public IEnumerable<CategoryDTO> GetChildCategories(int id)
        {
            var categories = _repository.Find(c => c.ParentId == id);

            return _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(categories);
        }

        public void Update(CategoryDTO categoryDTO)
        {
            _repository.Update(_mapper.Map<CategoryDTO, Category>(categoryDTO));
        }

        public IEnumerable<CategoryNestedNodeDTO> GetCategoryNestedNodes()
        {
            var categories = _repository.GetAll().ToList();
            List<CategoryNestedNodeDTO> nodes;
            nodes = categories.Where(c => c.ParentId == 0)
                .Select(c => new CategoryNestedNodeDTO
                {
                    Category = _mapper.Map<CategoryDTO>(c),
                    Childs = new List<CategoryNestedNodeDTO>()
                }).ToList();

            foreach (var node in nodes)
            {
                FillNodes(categories, node);
            }

            return nodes;
        }

        private void FillNodes(IEnumerable<Category> categories, CategoryNestedNodeDTO node)
        {
            var childs = categories.Where(c => c.ParentId == node.Category.CategoryId);
            if(childs.Count() == 0)
            {
                return;
            }

            node.Childs = childs.Select(c => new CategoryNestedNodeDTO {
                Category = _mapper.Map<CategoryDTO>(c),
                Childs = new List<CategoryNestedNodeDTO>()
            }).ToList();

            foreach (var child in node.Childs)
            {
                FillNodes(categories, child);
            }
        }
    }
}
