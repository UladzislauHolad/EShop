// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using Arch.IS4Host.Data;
using Arch.IS4Host.Models;
using AutoMapper;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Arch.IS4Host
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
         
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

           services.AddCors(options =>
            {
                options.AddPolicy("default", policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            //services.AddAuthentication()
            //   .AddJwtBearer(jwt =>
            //   {
            //       jwt.Authority = "http://localhost:5000";
            //       jwt.RequireHttpsMetadata = false;
            //       jwt.Audience = "http://localhost:5000/resources";
            //   });

            services.AddAutoMapper(typeof(Startup).Assembly);

            services.AddMvc();                

            services.Configure<IISOptions>(iis =>
            {
                iis.AuthenticationDisplayName = "Windows";
                iis.AutomaticAuthentication = false;
            });

            var builder = services.AddIdentityServer(options =>
                options.Discovery.CustomEntries.Add("custom_endpoint", "~/users")
                )
                // Настройка хранилища конфигураций
                .AddConfigurationStore(configDb =>
                    configDb.ConfigureDbContext = db =>
                        db.UseSqlServer(connectionString,
                        sql => sql.MigrationsAssembly(migrationsAssembly))
                )
                
                .AddOperationalStore(operationalDb =>
                    operationalDb.ConfigureDbContext = db =>
                        db.UseSqlServer(connectionString,
                        sql => sql.MigrationsAssembly(migrationsAssembly))
                )
                .AddAspNetIdentity<ApplicationUser>();
            
            if (Environment.IsDevelopment())
            {
                builder.AddDeveloperSigningCredential();
            }
            else
            {
                throw new Exception("need to configure key material");
            }
        }

        public void Configure(IApplicationBuilder app)
        {
            InitializDatabase(app);
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseCors("default");

            app.UseStaticFiles();
            app.UseIdentityServer();
            app.UseMvcWithDefaultRoute();
        }

        private void InitializDatabase(IApplicationBuilder app)
        {
            using( var serviceScope = 
                app.ApplicationServices
                    .GetService<IServiceScopeFactory>()
                    .CreateScope())
            {
                var persistedGrantDbContext = serviceScope.ServiceProvider
                    .GetRequiredService<PersistedGrantDbContext>();
                persistedGrantDbContext.Database.Migrate();

                var configDbContext = serviceScope.ServiceProvider
                    .GetRequiredService<ConfigurationDbContext>();
                configDbContext.Database.Migrate();

                if(!configDbContext.Clients.Any())
                {
                    foreach(var client in Config.GetClients()) 
                    {
                        configDbContext.Clients.Add(client.ToEntity());
                    }
                    configDbContext.SaveChanges();
                }

                if(!configDbContext.IdentityResources.Any())
                {
                    foreach(var res in Config.GetIdentityResources()) 
                    {
                        configDbContext.IdentityResources.Add(res.ToEntity());
                    }
                    configDbContext.SaveChanges();
                }

                if(!configDbContext.ApiResources.Any())
                {
                    foreach(var api in Config.GetApis()) 
                    {
                        configDbContext.ApiResources.Add(api.ToEntity());
                    }
                    configDbContext.SaveChanges();
                }
            }
        }
    }
}
