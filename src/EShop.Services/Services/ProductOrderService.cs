﻿using AutoMapper;
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
        IMapper _mapper;

        public ProductOrderService(IRepository<Product> productRepository,
            IRepository<Order> orderRepository, 
            IRepository<ProductOrder> productOrderRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _productOrderRepository = productOrderRepository;
            _mapper = mapper;
        }

        public void Create(ProductOrderDTO productOrderDTO)
        {
            var product = _productRepository.Get(productOrderDTO.ProductId);
            if(product != null)
            {
                var order = _orderRepository.Get(productOrderDTO.OrderId);
                if(order != null)
                {
                    var existedProductOrder = _productOrderRepository
                        .Find(po => po.ProductId == productOrderDTO.ProductId && po.OrderId == productOrderDTO.OrderId)
                        .SingleOrDefault();
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

                        _productRepository.Save();
                        _productOrderRepository.Create(_mapper.Map<ProductOrder>(productOrderDTO));
                    }
                }
            }
        }

        public void Delete(int id)
        {
            var productOrder = _productOrderRepository.Get(id);
            if(productOrder != null)
            {
                productOrder.Product.Count += productOrder.OrderCount;
                _productOrderRepository.Delete(id);
            }
        }

        public ProductOrderDTO GetProductOrder(int id)
        {
            return _mapper.Map<ProductOrderDTO>(_productOrderRepository.Get(id));
        }

        public void Update(ProductOrderDTO productOrderDTO)
        {
            var productOrder = _productOrderRepository.Get(productOrderDTO.ProductOrderId);
            var product = _productRepository.Get(productOrderDTO.ProductId);
            if(productOrder != null)
            {
                int newCount = product.Count + productOrder.OrderCount - productOrderDTO.OrderCount;
                if (newCount < 0)
                    throw new InvalidOperationException("OrderCount is not valid");
                productOrder.Name = product.Name;
                productOrder.Description = product.Description;
                productOrder.Price = product.Price;
                productOrder.Product.Count = newCount;
                productOrder.OrderCount = productOrderDTO.OrderCount;
                _productOrderRepository.Update(productOrder);
            }
        }
    }
}
