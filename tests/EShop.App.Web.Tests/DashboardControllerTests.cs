//using EShop.App.Web.Controllers;
//using EShop.Services.DTO;
//using EShop.Services.Infrastructure.Enums;
//using EShop.Services.Interfaces;
//using Microsoft.AspNetCore.Mvc;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using Xunit;

//namespace EShop.App.Web.Tests
//{
//    public class DashboardControllerTests
//    {
//        [Fact]
//        public void Index_Invoke_ReturnViewResult()
//        {
//            var service = new Mock<IOrderStatusChangeService>();
//            service.Setup(m => m.GetInfoByStatus(StatusStates.New)).Returns(new List<OrderStatusChartInfoDTO>());
//            service.Setup(m => m.GetInfoByStatus(StatusStates.Confirmed)).Returns(new List<OrderStatusChartInfoDTO>());
//            service.Setup(m => m.GetInfoByStatus(StatusStates.Paid)).Returns(new List<OrderStatusChartInfoDTO>());
//            service.Setup(m => m.GetInfoByStatus(StatusStates.Completed)).Returns(new List<OrderStatusChartInfoDTO>());
//            var controller = new DashboardController(service.Object);

//            var result = controller.Index();

//            Assert.True(result is JsonResult);
//        }
//    }
//}
