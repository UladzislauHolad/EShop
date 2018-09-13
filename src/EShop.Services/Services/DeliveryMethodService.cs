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
        private readonly IMapper _mapper;

        public DeliveryMethodService(IRepository<DeliveryMethod> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IEnumerable<DeliveryMethodDTO> GetDeliveryMethods()
        {
            return _mapper.Map<IEnumerable<DeliveryMethodDTO>>(_repository.GetAll().ToList());
        }
    }
}
