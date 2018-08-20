using AutoMapper;
using EShop.Data.Entities;
using EShop.Data.Interfaces;
using EShop.Services.DTO;
using EShop.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Services.Services
{
    public class OrderService : IOrderService
    {
        IRepository<Order> _repository;

        public OrderService(IRepository<Order> repository)
        {
            _repository = repository;
        }

        public IEnumerable<OrderDTO> GetOrders()
        {
            var mapper = GetMapper();

            return mapper.Map<IEnumerable<Order>, List<OrderDTO>>(_repository.GetAll());
        }
        private IMapper GetMapper()
        {
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Order, OrderDTO>()
                    .ForMember(dest => dest.Products,
                            opt => opt.MapFrom(src => src.Products));
                cfg.CreateMap<Product, ProductDTO>();
                cfg.CreateMap<OrderDTO, Order>();
            }).CreateMapper();

            return mapper;
        }
    }
}
