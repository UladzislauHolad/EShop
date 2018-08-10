using EShop.Data.Entities;
using EShop.Data.Interfaces;
using EShop.Data.Repositories;
using Ninject.Modules;

namespace EShop.Services.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        public ServiceModule()
        {}

        public override void Load()
        {
            Bind<IRepository<Product>>().To<ProductRepository>();
        }
    }
}
