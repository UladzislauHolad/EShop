using AutoMapper;
using EShop.App.Web.Models;
using EShop.App.Web.Profiles;
using EShop.Data.Entities;
using EShop.Data.Interfaces;
using EShop.Services.DTO;
using EShop.Services.Profiles;
using EShop.Services.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EShop.Services.Tests
{
    public class ProductOrderServiceTests
    {
        [Fact]
        public void Delete_InvokeId_ProductOrderDeleted()
        {
            var productOrder = new ProductOrder
            {
                ProductOrderId = 1,
                OrderId = 1,
                ProductId = 1,
                Name = "P2",
                Description = "Des2",
                Price = 1,
                OrderCount = 2,
                Product = new Product
                {
                    ProductId = 1,
                    Name = "P2",
                    Description = "Des2",
                    Price = 1,
                    Count = 2
                }
            };
            var productRepository = new Mock<IRepository<Product>>();
            //productRepository.Setup(repo => repo.Get(productOrderCreateModel.ProductId)).Returns(product);
            var orderRepository = new Mock<IRepository<Order>>();
            var productOrderRepository = new Mock<IRepository<ProductOrder>>();
            productOrderRepository.Setup(repo => repo.Get(1)).Returns(productOrder);
            var service = new ProductOrderService(productRepository.Object, orderRepository.Object, productOrderRepository.Object);

            service.Delete(1);

            productOrderRepository.Verify(m => m.Delete(1), Times.Once);
            productOrderRepository.Verify(m => m.Get(1), Times.Once);
        }

        //[Fact]
        //public void GetProductOrder_InvokeWithValidId_ProductOrderReturned()
        //{
        //    var mock = new Mock<IRepository<ProductOrder>>();
        //    mock.Setup(repo => repo.Get(1));
        //    var service = new ProductOrderService(mock.Object);

        //    service.GetProductOrder(1);

        //    mock.Verify(m => m.Get(1));
        //}

        //[Fact]
        //public void Update_InvokeWithValidInstance_ProductUpdated()
        //{
        //    var productOrder = new ProductOrder { ProductOrderId = 1 };
        //    var mapper = GetMapper();
        //    var mock = new Mock<IRepository<ProductOrder>>();
        //    mock.Setup(repo => repo.Update(productOrder));    
        //    var service = new ProductOrderService(mock.Object);

        //    service.Update(mapper.Map<ProductOrderDTO>(productOrder));

        //    mock.Verify(m => m.Update(It.Is<ProductOrder>(po => po.ProductOrderId == 1)), Times.Once);
        //}

        //[Fact]
        //public void Create_CreateProductOrder_ProductOrderWithRelationCreated()
        //{
        //    var productOrderDto = new ProductOrderDTO
        //    {
        //        ProductOrderId = 1,
        //        OrderId = 1,
        //        ProductId = 1,
        //        Name = "P2",
        //        Description = "Des2",
        //        Price = 1,
        //        OrderCount = 2,
        //        Product = new ProductDTO
        //        {
        //            ProductId = 1,
        //            Name = "P2",
        //            Description = "Des2",
        //            Price = 1,
        //            Count = 2
        //        }
        //    };
        //    var mapper = GetMapper();
        //    var productOrder = mapper.Map<ProductOrder>(productOrderDto);
        //    List<ProductOrder> productOrderList = null;
        //    var mock = new Mock<IRepository<ProductOrder>>();
        //    mock.Setup(repo => repo.Create(productOrder));
        //    mock.Setup(repo => repo.Find(It.IsAny<Func<ProductOrder, bool>>())).Returns(productOrderList);
        //    //mock.Setup(repo => repo.Update(productOrder));
        //    var service = new ProductOrderService(mock.Object);

        //    service.Create(productOrderDto);

        //    mock.Verify(m => m.Create(It.Is<ProductOrder>(po => po.Name == "P2")), Times.Once);
        //    mock.Verify(m => m.Find(It.IsAny<Func<ProductOrder, bool>>()), Times.Once);
        //}

        [Fact]
        public void Create_CreateExistProductOrder_ProductOrderWithRelationCreated()
        {
            var mapper = GetMapper();
            var productOrderCreateModel = new ProductOrderCreateViewModel
            {
                ProductOrderId = 1,
                OrderId = 1,
                ProductId = 1,
                OrderCount = 2
            };
            var newProductOrderDto = mapper.Map<ProductOrderDTO>(productOrderCreateModel);
            Order order = new Order { OrderId = 1 };
            Product product = new Product
            {
                ProductId = 1,
                Name = "P2",
                Description = "Des2",
                Price = 1,
                Count = 2
            };
            var existedProductOrderDto = new ProductOrderDTO
            {
                ProductOrderId = 1,
                OrderId = 1,
                ProductId = 1,
                Name = "P2",
                Description = "Des2",
                Price = 1,
                OrderCount = 2,
                Product = new ProductDTO
                {
                    ProductId = 1,
                    Name = "P2",
                    Description = "Des2",
                    Price = 1,
                    Count = 2
                }
            };
            var productOrder = mapper.Map<ProductOrder>(existedProductOrderDto);
            List<ProductOrder> productOrderList = new List<ProductOrder>();
            productOrderList.Add(productOrder);
            var productRepository = new Mock<IRepository<Product>>();
            productRepository.Setup(repo => repo.Get(productOrderCreateModel.ProductId)).Returns(product);
            var orderRepository = new Mock<IRepository<Order>>();
            orderRepository.Setup(repo => repo.Get(productOrderCreateModel.OrderId)).Returns(order);
            var productOrderRepository = new Mock<IRepository<ProductOrder>>();
            productOrderRepository.Setup(repo => repo.Find(It.IsAny<Func<ProductOrder, bool>>())).Returns(productOrderList);
            productOrderRepository.Setup(repo => repo.Update(productOrder));
            var service = new ProductOrderService(productRepository.Object, orderRepository.Object, productOrderRepository.Object);

            service.Create(newProductOrderDto);

            productRepository.Verify(m => m.Get(productOrderCreateModel.ProductId), Times.Once);
            orderRepository.Verify(m => m.Get(productOrderCreateModel.OrderId), Times.Once);            
            productOrderRepository.Verify(m => m.Find(It.IsAny<Func<ProductOrder, bool>>()), Times.Once);
            productOrderRepository.Verify(m => m.Update(It.Is<ProductOrder>(po => po.OrderCount == 4)), Times.Once);
        }

        [Fact]
        public void Create_CreateNotExistProductOrder_ProductOrderWithRelationCreated()
        {
            var mapper = GetMapper();
            var productOrderCreateModel = new ProductOrderCreateViewModel
            {
                ProductOrderId = 1,
                OrderId = 1,
                ProductId = 1,
                OrderCount = 2
            };
            var newProductOrderDto = mapper.Map<ProductOrderDTO>(productOrderCreateModel);
            Order order = new Order { OrderId = 1 };
            Product product = new Product
            {
                ProductId = 1,
                Name = "P2",
                Description = "Des2",
                Price = 1,
                Count = 2
            };

            var productOrderDto = new ProductOrderDTO
            {
                ProductOrderId = 1,
                OrderId = 1,
                ProductId = 1,
                Name = "P2",
                Description = "Des2",
                Price = 1,
                OrderCount = 4,
                Product = new ProductDTO
                {
                    ProductId = 1,
                    Name = "P2",
                    Description = "Des2",
                    Price = 1,
                    Count = 0
                }
            };
            var productOrder = mapper.Map<ProductOrder>(productOrderDto);
            List<ProductOrder> productOrderList = null;
            var productRepository = new Mock<IRepository<Product>>();
            productRepository.Setup(repo => repo.Get(productOrderCreateModel.ProductId)).Returns(product);
            var orderRepository = new Mock<IRepository<Order>>();
            orderRepository.Setup(repo => repo.Get(productOrderCreateModel.OrderId)).Returns(order);
            var productOrderRepository = new Mock<IRepository<ProductOrder>>();
            productOrderRepository.Setup(repo => repo.Find(It.IsAny<Func<ProductOrder, bool>>())).Returns(productOrderList);
            productOrderRepository.Setup(repo => repo.Create(productOrder));
            var service = new ProductOrderService(productRepository.Object, orderRepository.Object, productOrderRepository.Object);

            service.Create(newProductOrderDto);

            productRepository.Verify(m => m.Get(productOrderCreateModel.ProductId), Times.Once);
            orderRepository.Verify(m => m.Get(productOrderCreateModel.OrderId), Times.Once);
            productOrderRepository.Verify(m => m.Find(It.IsAny<Func<ProductOrder, bool>>()), Times.Once);
            productOrderRepository.Verify(m => m.Create(It.IsAny<ProductOrder>()), Times.Once);
        }

        private IMapper GetMapper()
        {
            var mapper = new MapperConfiguration(cfg =>
            {
                //cfg.AddProfile(new ProductOrderProfile());
                cfg.AddProfile(new ProductProfile());
                cfg.AddProfile(new ProductOrderCreateViewModelProfile());
            }).CreateMapper();

            return mapper;
        }
    }
}
