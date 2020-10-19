using System;
using System.Collections.Generic;
using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace ArchitectureProject.Common.Extensions
{
    public static class DbContextRegistration
    {
        public static void RegisterDbContext<TContext>(this ContainerBuilder builder, string connectionName, string migrationAssemblyName,bool useInMemoryDatabase = false)
            where TContext : DbContext
        {
            builder.Register(componentContext =>
                {
                    var serviceProvider = componentContext.Resolve<IServiceProvider>();
                    var configuration = componentContext.Resolve<IConfiguration>();
                    var dbContextOptions =
                        new DbContextOptions<TContext>(new Dictionary<Type, IDbContextOptionsExtension>());

                    var optionsBuilder = new DbContextOptionsBuilder<TContext>(dbContextOptions)
                        .UseApplicationServiceProvider(serviceProvider);

                    if (useInMemoryDatabase)
                    {
                        optionsBuilder.UseInMemoryDatabase(configuration.GetConnectionString(connectionName));
                    }
                    else
                    {
                        optionsBuilder.UseSqlServer(configuration.GetConnectionString(connectionName),
                            serverOptions => serverOptions.MigrationsAssembly(migrationAssemblyName));
                    }

                    return optionsBuilder.Options;
                }).As<DbContextOptions<TContext>>()
                .InstancePerLifetimeScope();

            builder.Register(context => context.Resolve<DbContextOptions<TContext>>())
                .As<DbContextOptions>()
                .InstancePerLifetimeScope();

            builder.RegisterType<TContext>()
                .AsSelf()
                .SingleInstance()
                .PropertiesAutowired(PropertyWiringOptions.PreserveSetValues);
        }
    }
}
