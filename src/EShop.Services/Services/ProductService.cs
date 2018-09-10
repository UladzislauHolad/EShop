using EShop.Services.DTO;
using EShop.Services.Interfaces;
using System.Collections.Generic;
using EShop.Data.Interfaces;
using EShop.Data.Entities;
using EShop.Services.Infrastructure;
using AutoMapper;
using System.Linq;

namespace EShop.Services.Services
{
    public class ProductService : IProductService
    {
        IRepository<Product> _repository { get; set; }
        IMapper _mapper;

                                                             

        public ProductService(IRepository<Product> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public ProductDTO GetProduct(int? id)
        {
            
            Product p = _repository.Get(id.Value);
            
            return _mapper.Map<Product, ProductDTO>(p);
        }

        public IEnumerable<ProductDTO> GetProducts()
        {
            var prods = _mapper.Map<IEnumerable<Product>, List<ProductDTO>>(_repository.GetAll().Where(p => p.IsDeleted == false));
            return prods;
        }

        public void Add(ProductDTO productDTO)
        {
            _repository.Create(_mapper.Map<ProductDTO, Product>(productDTO));
        }

        public void Update(ProductDTO productDTO)
        {
            _repository.Update(_mapper.Map<ProductDTO, Product>(productDTO));
        }

        public void Delete(int id)
        {
            var product = _repository.Get(id);
            if(product != null)
            {
                product.IsDeleted = true;
                product.ProductCategories = null;
                _repository.Save();
            }
        }

        public IEnumerable<ProductDTO> GetProductsByCategoryId(int id)
        {
            var allProducts = _repository.GetAll();
            var products = allProducts.Where(p => p.ProductCategories.Any(c => c.CategoryId == id) == true && p.IsDeleted == false);

            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public List<object> GetCategoriesWithCountOfProducts()
        {
            var products = _repository.GetAll().Where(p => p.IsDeleted == false);
            var count = products.Count();
            var productCategories = products.Select(p => p.ProductCategories);
            var categories = products.SelectMany(p => p.ProductCategories).Select(c => c.Category).Distinct();
            var query = productCategories
                .SelectMany(x => x)
                .GroupBy(y => y.CategoryId)
                .Select(g => new
                {
                    g.Key,
                    Count = g.Count()
                });
            var result = query.Join(categories, o => o.Key, i => i.CategoryId, (o, i) => new { Name = i.Name, o.Count });

            return result.ToList<object>();
        }
    }
}
