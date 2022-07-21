
using ArchitectureProject.Api;
using ArchitectureProject.Domain.Models;
using ArchitectureProject.Domain.Static;
using ArchitectureProject.Infrastructure;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureWebHostDefaults(webBuilder => {
    webBuilder
        .UseContentRoot(Directory.GetCurrentDirectory())
        .UseIISIntegration();
});

startup.ConfigureServices(builder.Services);

var app = builder.Build();

//app.ConfigureWebHostDefaults()

startup.Configure(app, app.Environment);

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<ArchitectureProjectDbContext>();

        context.Database.EnsureCreated();

        if (context.Database.IsSqlServer())
        {
            context.Database.Migrate();

            if (!context.Roles.Any())
            {
                context.Roles.AddRange(new Role[]
                {
                    new Role()
                    {
                        AddedDate = DateTime.Now,
                        Name = "Administrator",
                        RoleId = Roles.Administrator,
                    },
                    new Role()
                    {
                        AddedDate = DateTime.Now,
                        Name = "Normal user",
                        RoleId = Roles.NormalUser,
                    }
                });

                context.SaveChanges();
            }
        }
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

        logger.LogError(ex, "An error occurred while migration database.");

        throw;
    }
}

app.Run();
