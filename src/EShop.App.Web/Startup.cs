using EShop.Data.EF;
using EShop.Data.Entities;
using EShop.Data.Interfaces;
using EShop.Data.Repositories;
using EShop.Services.Interfaces;
using EShop.Services.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using FluentValidation.AspNetCore;
using EShop.Data.EF.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Reflection;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using IdentityServer4.AccessTokenValidation;

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
            services.AddDbContext<EShopContext>(options =>
                options
                .UseSqlServer(AppConfiguration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<EShopContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.Authority = AppConfiguration["IdentityServerAddress"];
                o.Audience = "http://localhost:5000/resources";
                o.RequireHttpsMetadata = false;
            });

            services.AddTransient<IDbContext, EShopContext>();

            services.AddTransient<IRepository<Product>, ProductRepository>();
            services.AddTransient<IRepository<Category>, CategoryRepository>();
            services.AddTransient<IRepository<Order>, OrderRepository>();
            services.AddTransient<IRepository<ProductOrder>, ProductOrderRepository>();
            services.AddTransient<IRepository<Customer>, CustomerRepository>();
            services.AddTransient<IRepository<PaymentMethod>, PaymentMethodRepository>();
            services.AddTransient<IRepository<DeliveryMethod>, DeliveryMethodRepository>();
            services.AddTransient<IRepository<OrderStatusChange>, OrderStatusChangeRepository>();
            services.AddTransient<IRepository<PickupPoint>, PickupPointRepository>();

            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IProductOrderService, ProductOrderService>();
            services.AddTransient<ICustomerService, СustomerService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IPaymentMethodService, PaymentMethodService>();
            services.AddTransient<IDeliveryMethodService, DeliveryMethodService>();
            services.AddTransient<IOrderStatusChangeService, OrderStatusChangeService>();
            services.AddTransient<IPickupPointService, PickupPointService>();

            services.AddAutoMapper(typeof(Startup).Assembly, typeof(Services.Profiles.CustomerDTOProfile).Assembly);

            services.AddCors(options =>
            {
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins("*")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });


            services.AddMvc()
                .AddFluentValidation(fvc => 
                    fvc.RegisterValidatorsFromAssemblyContaining<Startup>());
            services.AddMvc().AddXmlSerializerFormatters();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "EShop API"
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            //var setting = AppConfiguration.GetSection<>();
            //services.AddSingleton<>();



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("default");

            app.UseAuthentication();
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });


            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default", template: "{controller}/{action=index}/{id}");
            });

            app.UseSpa(cfg =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501
                cfg.Options.DefaultPage = "/spa";
                cfg.Options.SourcePath = "ClientApp";
                if (env.IsDevelopment())
                {
                    cfg.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
