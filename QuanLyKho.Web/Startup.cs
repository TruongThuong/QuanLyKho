using System;
using System.Reflection;
using Autofac;
using Autofac.Framework.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using QuanLyKho.Data;
using QuanLyKho.Data.Infrastructure;
using QuanLyKho.Data.Repositories;
using QuanLyKho.Service;
using QuanLyKho.Web.Api;

namespace QuanLyKho.Web
{
    public class Startup
    {
        public IContainer container { get; private set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<QuanLyKhoDbContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddMvc().AddControllersAsServices();

            var builder = new ContainerBuilder();

            builder.RegisterType<QuanLyKhoDbContext>().AsSelf()
                .InstancePerRequest();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>()
                .InstancePerRequest();
            builder.RegisterType<DbFactory>().As<IDbFactory>()
                .InstancePerRequest();
            //API Controller

            builder.RegisterAssemblyTypes(typeof(PostCategoryController).GetTypeInfo().Assembly)
                .Where(
                    t =>
                        typeof(Controller).IsAssignableFrom(t) &&
                        t.Name.EndsWith("Controller", StringComparison.Ordinal))
                        .PropertiesAutowired();

            //services
            builder.RegisterAssemblyTypes(typeof(PostCategoryService).GetTypeInfo().Assembly)
              .Where(t => t.Name.EndsWith("Service"))
              .AsImplementedInterfaces()
              .InstancePerRequest();

            //Repository            

            builder.RegisterAssemblyTypes(typeof(PostCategoryRepository).GetTypeInfo().Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerRequest();

            var container = builder.Build();

            return container.Resolve<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime appLifetime)
        {

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
            appLifetime.ApplicationStopped.Register(() => this.container.Dispose());
        }
    }
}