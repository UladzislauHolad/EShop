using EShop.App.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EShop.App.Web.Tests
{
    public class DashboardControllerTests
    {
        [Fact]
        public void Index_Invoke_ReturnViewResult()
        {
            var controller = new DashboardController();

            var result = controller.Index();

            Assert.True(result is ViewResult);
        }
    }
}
