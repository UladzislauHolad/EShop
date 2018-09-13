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
    public class PickupPointService : IPickupPointService
    {
        readonly IRepository<PickupPoint> _repository;
        readonly IMapper _mapper;

        public PickupPointService(IRepository<PickupPoint> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IEnumerable<PickupPointDTO> GetPickupPoints()
        {
            return _mapper.Map<IEnumerable<PickupPointDTO>>(_repository.GetAll());
        }
    }
}
