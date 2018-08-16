using EShop.App.Web.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EShop.App.Web.Tests
{
    public class StartupTests
    {
        //[Fact]
        //public void ConfigureServices_RegisterDependencies_RegisterDependenciesCorrect()
        //{
        //    Mock<IConfigurationSection> configurationSectionStub = new Mock<IConfigurationSection>();
        //    configurationSectionStub.Setup(x => x["DefaultConnection"]).Returns("Server=golodv;Database=EShopDB;Trusted_Connection=True;");
        //    Mock<IConfiguration> configurationStub = new Mock<IConfiguration>();
        //    configurationStub.Setup(x => x.GetSection("ConnectionStrings")).Returns(configurationSectionStub.Object);
        //    IServiceCollection services = new ServiceCollection();
        //    Mock<IHostingEnvironment> env = new Mock<IHostingEnvironment>();
        //    var target = new Startup(env.Object) { AppConfiguration = configurationStub.Object };

        //    //  Act

        //    target.ConfigureServices(services);
        //    //  Mimic internal asp.net core logic.
        //    services.AddTransient<ProductController>();

        //    //  Assert

        //    var serviceProvider = services.BuildServiceProvider();
        //    var controller = serviceProvider.GetService<ProductController>();

        //    Assert.NotNull(controller);
        //}
    }
}
