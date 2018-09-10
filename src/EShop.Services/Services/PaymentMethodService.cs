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
    public class PaymentMethodService : IPaymentMethodService
    {
        IRepository<PaymentMethod> _repository;
        IMapper _mapper;

        public PaymentMethodService(IRepository<PaymentMethod> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IEnumerable<PaymentMethodDTO> GetPaymentMethods()
        {
            var paymentMethods = _repository.GetAll().ToList();

            return _mapper.Map<IEnumerable<PaymentMethodDTO>>(paymentMethods);
        }
    }
}
