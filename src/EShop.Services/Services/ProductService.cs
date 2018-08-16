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

                                                             

        public ProductService(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public ProductDTO GetProduct(int? id)
        {
            if(id == null)
            {
                throw new ValidationException("Не установлен id продукта", "");
            }
            Product p = _repository.Get(id.Value);
            if(p == null)
            {
                throw new ValidationException("Продукт не найден", "");
            }

            var mapper = GetMapper();

            return mapper.Map<Product, ProductDTO>(p);
        }

        public IEnumerable<ProductDTO> GetProducts()
        {
            var mapper = GetMapper();
            var prods = mapper.Map<IEnumerable<Product>, List<ProductDTO>>(_repository.GetAll());
            return prods;
        }

        public void Add(ProductDTO productDTO)
        {
            var mapper = GetMapper();

            _repository.Create(mapper.Map<ProductDTO, Product>(productDTO));
        }

        public void Update(ProductDTO productDTO)
        {
            var mapper = GetMapper();

            _repository.Update(mapper.Map<ProductDTO, Product>(productDTO));
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public IEnumerable<ProductDTO> GetProductsByCategoryId(int id)
        {
            var mapper = GetMapper();

            var allProducts = _repository.GetAll();
            var products = allProducts.Where(p => p.ProductCategories.Any(c => c.CategoryId == id) == true);

            return mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        private IMapper GetMapper()
        {
            var mapper = new MapperConfiguration((cfg) =>
            {
                cfg.CreateMap<Product, ProductDTO>()
                    .ForMember(dest => dest.Categories,
                        opt => opt.MapFrom(src => src.ProductCategories));
                cfg.CreateMap<ProductDTO, Product>()
                    .ForMember(dest => dest.ProductCategories,
                        opt => opt.MapFrom(src => src.Categories));

                cfg.CreateMap<ProductCategory, CategoryDTO>()
                    .ForMember(dest => dest.CategoryId,
                                opt => opt.MapFrom(src => src.CategoryId))
                    .ForMember(dest => dest.Name,
                                opt => opt.MapFrom(src => src.Category.Name));

                cfg.CreateMap<CategoryDTO, ProductCategory>()
                    .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId));
            }
            ).CreateMapper();

            return mapper;
        }
    }
}
