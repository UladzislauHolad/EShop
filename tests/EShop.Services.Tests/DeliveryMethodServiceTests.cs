﻿using AutoMapper;
using EShop.Data.Entities;
using EShop.Data.Interfaces;
using EShop.Services.DTO;
using EShop.Services.Profiles;
using EShop.Services.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace EShop.Services.Tests
{
    public class DeliveryMethodServiceTests
    {
        [Fact]
        public void GetDeliveryMethods_Invoke_ReturnIEnumerableDeliveryMethodDTO()
        {
            var repository = new Mock<IRepository<DeliveryMethod>>();
            repository.Setup(m => m.GetAll()).Returns(new List<DeliveryMethod>().AsQueryable());
            var service = new DeliveryMethodService(repository.Object, GetMapper());

            var result = service.GetDeliveryMethods();

            Assert.True(result is IEnumerable<DeliveryMethodDTO>);
        }

        private IMapper GetMapper()
        {
            var _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DeliveryMethodDTOProfile());
            }).CreateMapper();

            return _mapper;
        }
    }
}
