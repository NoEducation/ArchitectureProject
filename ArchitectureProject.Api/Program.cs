using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ArchitectureProject.Infrastructure;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ArchitectureProject.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
           var host = CreateHostBuilder(args).Build();

           using (var scope = host.Services.CreateScope())
           {
               var services = scope.ServiceProvider;

               try
               {
                   var context = services.GetRequiredService<ArchitectureProjectDbContext>();

                   context.Database.EnsureCreated();

                   if (context.Database.IsSqlServer())
                   {
                       context.Database.Migrate();
                   }

               }
               catch (Exception ex)
               {
                   var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

                   logger.LogError(ex, "An error occurred while migration database.");

                   throw;
               }
           }

           host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseContentRoot(Directory.GetCurrentDirectory())
                        .UseIISIntegration()
                        .UseStartup<Startup>();
                });
    }
}
