using System.Collections.Generic;
using EShop.Services.DTO;
using EShop.Services.Interfaces;
using System;
using EShop.Data.Interfaces;
using EShop.Data.Entities;
using AutoMapper;
using EShop.Services.Profiles;
using System.Linq;

namespace EShop.Services.Services
{
    public class DeliveryMethodService : IDeliveryMethodService
    {
        private readonly IRepository<DeliveryMethod> _repository;

        public DeliveryMethodService(IRepository<DeliveryMethod> repository)
        {
            _repository = repository;
        }

        public IEnumerable<DeliveryMethodDTO> GetDeliveryMethods()
        {
            var mapper = GetMapper();

            return mapper.Map<IEnumerable<DeliveryMethodDTO>>(_repository.GetAll().ToList());
        }

        private IMapper GetMapper()
        {
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DeliveryMethodDTOProfile());
            }).CreateMapper();

            return mapper;
        }
    }
}
