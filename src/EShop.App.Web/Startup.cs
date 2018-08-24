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

            services.AddTransient<IDbContext, EShopContext>();
            services.AddTransient<IRepository<Product>, ProductRepository>();
            services.AddTransient<IRepository<Category>, CategoryRepository>();
            services.AddTransient<IRepository<Order>, OrderRepository>();
            services.AddTransient<IRepository<ProductOrder>, ProductOrderRepository>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IProductOrderService, ProductOrderService>();
            services.AddAutoMapper(typeof(Startup));
            services.AddMvc()
                .AddFluentValidation(fvc => 
                    fvc.RegisterValidatorsFromAssemblyContaining<Startup>());
            services.AddMvc();            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();
            app.UseDeveloperExceptionPage();
            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "editCategory",
                    template: "Categories/Edit{id}",
                    defaults: new { controller = "Category", action = "Edit" });
                routes.MapRoute(
                    name: "showCategories",
                    template: "Categories/Page{page}",
                    defaults: new { controller = "Category", action = "Index" });
                routes.MapRoute(
                    name: "createCategory",
                    template: "Categories",
                    defaults: new { controller = "Category", action = "Create" });
                routes.MapRoute(
                    name: "categorySelect",
                    template: "Category/CategorySelect",
                    defaults: new { controller = "Category", action = "CategorySelect" });
                routes.MapRoute(
                    name: "getChildCategories",
                    template: "Childs/{id}",
                    defaults: new { controller = "Category", action = "Childs" });
                routes.MapRoute(
                    name: "showProducts",
                    template: "Products/Product{id}",
                    defaults: new { controller = "Product", action = "Products" });
                routes.MapRoute(
                    name: "edit",
                    template: "Products/Edit{id}",
                    defaults: new { controller = "Product", action = "Edit" });
                routes.MapRoute(
                    name: "pagination",
                    template: "Products/Page{page}",
                    defaults: new { controller = "Product", action = "Index"});
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Product}/{action=Index}/{id?}");
            });
        }
    }
}
