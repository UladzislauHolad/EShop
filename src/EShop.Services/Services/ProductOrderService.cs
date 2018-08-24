using AutoMapper;
using EShop.Data.Entities;
using EShop.Data.Interfaces;
using EShop.Services.DTO;
using EShop.Services.Interfaces;
using EShop.Services.Profiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Services.Services
{
    public class ProductOrderService : IProductOrderService
    {
        IRepository<ProductOrder> _repository;

        public ProductOrderService(IRepository<ProductOrder> repository)
        {
            _repository = repository;
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public ProductOrderDTO GetProductOrder(int id)
        {
            var mapper = GetMapper();

            return mapper.Map<ProductOrderDTO>(_repository.Get(id));
        }

        public void Update(ProductOrderDTO productOrderDTO)
        {
            var mapper = GetMapper();

            _repository.Update(mapper.Map<ProductOrder>(productOrderDTO));
        }

        private IMapper GetMapper()
        {
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ProductOrderProfile());
            }).CreateMapper();

            return mapper;
        }
    }
}
