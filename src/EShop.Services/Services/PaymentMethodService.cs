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

        public PaymentMethodService(IRepository<PaymentMethod> repository)
        {
            _repository = repository;
        }

        public IEnumerable<PaymentMethodDTO> GetPaymentMethods()
        {
            var mapper = GetMapper();

            var paymentMethods = _repository.GetAll().ToList();

            return mapper.Map<IEnumerable<PaymentMethodDTO>>(paymentMethods);
        }

        private IMapper GetMapper()
        {
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new PaymentMethodDTOProfile());
            }).CreateMapper();

            return mapper;
        }
    }
}
