using AutoMapper;
using EShop.Data.Entities;
using EShop.Data.Interfaces;
using EShop.Services.DTO;
using EShop.Services.Interfaces;
using EShop.Services.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EShop.Services.Services
{
    public class ProductOrderService : IProductOrderService
    {
        IRepository<Product> _productRepository;
        IRepository<Order> _orderRepository;
        IRepository<ProductOrder> _productOrderRepository;

        public ProductOrderService(IRepository<Product> productRepository,
            IRepository<Order> orderRepository, 
            IRepository<ProductOrder> productOrderRepository)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _productOrderRepository = productOrderRepository;
        }

        public void Create(ProductOrderDTO productOrderDTO)
        {
            var mapper = GetMapper();

            var product = _productRepository.Get(productOrderDTO.ProductId);
            if(product != null)
            {
                var order = _orderRepository.Get(productOrderDTO.OrderId);
                if(order != null)
                {
                    var existedProductOrder = _productOrderRepository.Find(po => po.ProductId == productOrderDTO.ProductId).SingleOrDefault();
                    if(existedProductOrder != null)
                    {
                        existedProductOrder.Name = product.Name;
                        existedProductOrder.Description = product.Description;
                        existedProductOrder.Price = product.Price;
                        existedProductOrder.OrderCount += productOrderDTO.OrderCount;
                        product.Count -= productOrderDTO.OrderCount;

                        _productRepository.Save();
                        _productOrderRepository.Update(existedProductOrder);
                    }
                    else
                    {
                        productOrderDTO.Name = product.Name;
                        productOrderDTO.Description = product.Description;
                        productOrderDTO.Price = product.Price;
                        product.Count -= productOrderDTO.OrderCount;

                        _productRepository.Update(product);
                        _productOrderRepository.Create(mapper.Map<ProductOrder>(productOrderDTO));
                    }
                }
            }
        }

        public void Delete(int id)
        {
            _productOrderRepository.Delete(id);
        }

        public ProductOrderDTO GetProductOrder(int id)
        {
            var mapper = GetMapper();

            return mapper.Map<ProductOrderDTO>(_productOrderRepository.Get(id));
        }

        public void Update(ProductOrderDTO productOrderDTO)
        {
            var mapper = GetMapper();

            _productOrderRepository.Update(mapper.Map<ProductOrder>(productOrderDTO));
        }

        private IMapper GetMapper()
        {
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ProductOrderProfile());
                cfg.AddProfile(new ProductProfile());
            }).CreateMapper();

            return mapper;
        }
    }
}
