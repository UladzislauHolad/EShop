﻿using AutoMapper;
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

        public object GetCategoryNameWithCountOfProducts()
        {
            var categories = _repository.GetAll();
            var result = categories.Select(c => new { Name = c.Name, Value = c.ProductCategories.Count });

            return result;
        }

        public IEnumerable<CategoryNodeDTO> GetCategoryNodes(int parentId)
        {
            var categoryNodes = _repository.GetAll().Where(c => c.ParentId == parentId)
                .Select(c => new CategoryNodeDTO
                {
                    CategoryId = c.CategoryId,
                    Name = c.Name
                }).ToList();

            foreach (var categoryNode in categoryNodes)
            {
                categoryNode.HasChilds = _repository.GetAll().Any(c => c.ParentId == categoryNode.CategoryId);
            }


            return categoryNodes;
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
    }
}
