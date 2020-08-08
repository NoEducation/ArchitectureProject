using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ArchitectureProject.Api.Modules;
using ArchitectureProject.Common.Extensions;
using ArchitectureProject.Domain.Models;
using ArchitectureProject.Infrastructure;
using ArchitectureProject.Logic.WeatherForecasts.Queries;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ArchitectureProject.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public ILifetimeScope Container { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddHttpContextAccessor();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {

            builder.RegisterDbContext<ArchitectureProjectDbContext>("DefaultConnection",
                typeof(Startup).Assembly.GetName().Name);
            builder.RegisterModule(new CommonModule());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            this.Container = app.ApplicationServices.GetAutofacRoot();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
