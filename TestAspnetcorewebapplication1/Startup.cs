using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestAspnetcorewebapplication1.Configurations;
using TestAspnetcorewebapplication1.Quickstart;

namespace TestAspnetcorewebapplication1
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = @"Data Source = (LocalDb)\MSSQLLocalDB; database = Test.IdentityServer4.EntityFramework; trusted_connection = yes; ";
            var migrationAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddMvc();

            services.AddDbContext<ApplicationDBContext>(builder =>
            builder.UseSqlServer(connectionString, sqlOptions =>
                                sqlOptions.MigrationsAssembly(migrationAssembly)));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDBContext>();

            services.AddIdentityServer()
                .AddOperationalStore(options =>
                options.ConfigureDbContext = builder =>
                builder.UseSqlServer(connectionString, sqlOptions =>
                 sqlOptions.MigrationsAssembly(migrationAssembly)))
                 .AddConfigurationStore(options =>
                options.ConfigureDbContext = builder =>
                builder.UseSqlServer(connectionString, sqlOptions =>
               sqlOptions.MigrationsAssembly(migrationAssembly)))
                    //.AddInMemoryClients(Clients.Get())
                    //.AddInMemoryIdentityResources(Configurations.Resources.GetIdentityResources())
                    //.AddInMemoryApiResources(Configurations.Resources.GetApiResources())
                    //.AddTestUsers(Users.GetTestUsers())
                    .AddAspNetIdentity<ApplicationUser>()
                    .AddDeveloperSigningCredential();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            InitDBTestDatabase(app);

            app.UseIdentityServer();

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }

        private void InitDBTestDatabase(IApplicationBuilder app) {

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
                serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>().Database.Migrate();

                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

                if (!context.Clients.Any())
                {
                    foreach (var client in Clients.Get())
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.ApiResources.Any())
                {
                    foreach (var apiRes in Configurations.Resources.GetApiResources())
                    {
                        context.ApiResources.Add(apiRes.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.IdentityResources.Any())
                {
                    foreach (var identityRes in Configurations.Resources.GetIdentityResources())
                    {
                        context.IdentityResources.Add(identityRes.ToEntity());
                    }
                    context.SaveChanges();
                }


            }

        }
    }
}
