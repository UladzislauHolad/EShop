using EShop.Data.Entities;
using EShop.Data.Interfaces;
using EShop.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Text;

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
