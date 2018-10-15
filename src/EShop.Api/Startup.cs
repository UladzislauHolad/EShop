using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using EShop.Api.Infrastructure.Middleware;
using EShop.Data.EF;
using EShop.Data.EF.Interfaces;
using EShop.Data.Entities;
using EShop.Data.Interfaces;
using EShop.Data.Repositories;
using EShop.Services.Interfaces;
using EShop.Services.Services;
using FluentValidation.AspNetCore;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace EShop.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json").Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<EShopContext>(options =>
                options
                .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMvcCore()
                .AddAuthorization()
                .AddJsonFormatters();

            services.AddDistributedMemoryCache();
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                 .AddIdentityServerAuthentication(options =>
                 {
                     options.Authority = Configuration.GetSection("IdentityServerAddress").Value;
                     options.RequireHttpsMetadata = false;
                     options.ApiName = "apiApp";
                     options.ApiSecret = "secret";
                     options.EnableCaching = true;
                     options.CacheDuration = TimeSpan.FromSeconds(30);
                 });

            services.AddTransient<IDbContext, EShopContext>();

            services.AddTransient<IRepository<Product>, ProductRepository>();
            services.AddTransient<IRepository<Category>, CategoryRepository>();
            services.AddTransient<IRepository<Order>, OrderRepository>();
            services.AddTransient<IRepository<PaymentMethod>, PaymentMethodRepository>();
            services.AddTransient<IRepository<DeliveryMethod>, DeliveryMethodRepository>();
            services.AddTransient<IRepository<PickupPoint>, PickupPointRepository>();
            services.AddTransient<IRepository<Customer>, CustomerRepository>();
            services.AddTransient<IRepository<OrderStatusChange>, OrderStatusChangeRepository>();
            services.AddTransient<IRepository<ProductOrder>, ProductOrderRepository>();

            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IPaymentMethodService, PaymentMethodService>();
            services.AddTransient<IDeliveryMethodService, DeliveryMethodService>();
            services.AddTransient<IPickupPointService, PickupPointService>();
            services.AddTransient<IOrderStatusChangeService, OrderStatusChangeService>();
            services.AddTransient<IProductOrderService, ProductOrderService>();

            services.AddAutoMapper(typeof(Startup).Assembly, typeof(Services.Profiles.CustomerDTOProfile).Assembly);

            services.AddCors(options =>
            {
                options.AddPolicy("default", policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            services.AddMvc()
                .AddFluentValidation(fvc =>
                    fvc.RegisterValidatorsFromAssemblyContaining<Startup>())
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });

                var xmlFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.AddFluentValidationRules();
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("default");

            app.UseDeveloperExceptionPage();

            app.UseAuthentication();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseEntityExistExceptionHandlerMiddleware();

            app.UseMvc();
        }
    }
}
