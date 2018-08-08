using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Data.EF;
using EShop.Data.Entities;
using EShop.Data.Interfaces;
using EShop.Data.Repositories;
using EShop.Services.Interfaces;
using EShop.Services.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;

namespace EShop.App.Web
{
    public class Startup
    {
        public IConfiguration AppConfiguration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            AppConfiguration = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json").Build();
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddDbContext<ProductContext>(options =>
                options.UseSqlServer(AppConfiguration.GetConnectionString("DefaultConnection")));
            services.AddTransient<IRepository<Product>, ProductRepository>();
            services.AddTransient<IService, Service>();
            services.AddMvc();
            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();
            app.UseDeveloperExceptionPage();
            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}");
            });
        }
    }
}
