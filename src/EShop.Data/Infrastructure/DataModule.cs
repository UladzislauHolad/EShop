using EShop.Data.EF;
using EShop.Data.EF.Interfaces;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Data.Infrastructure
{
    public class DataModule : NinjectModule
    {
        public DataModule()
        {
        }
        public override void Load()
        {
            Bind<IDbContext>().To<EShopContext>();
        }
    }
}
