using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using ArchitectureProject.Api;
using ArchitectureProject.Api.Modules;
using ArchitectureProject.Common.Extensions;
using ArchitectureProject.Infrastructure;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using Respawn;

namespace ArchitectureProject.IntegrationTests
{
    public class IntegrationTestBase
    {
        protected IConfiguration Configuration { get; set; }
        protected ILifetimeScope Container { get; private set; }
        protected IMediator Mediator { get; private set; }

        protected ArchitectureProjectDbContext  DbContext { get; private set; }

        private static Checkpoint _checkpoint;
        private static string _currentUserId;


        [SetUp]
        public void Setup()
        {
            var confBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();

            Configuration = confBuilder.Build();

            var services = new ServiceCollection();

            services.AddLogging();

            ContainerBuilder builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterModule(new CommonModule());
            builder.RegisterInstance(Configuration).As<IConfiguration>();
            builder.RegisterDbContext<ArchitectureProjectDbContext>("DefaultConnection",
                Assembly.Load("ArchitectureProject.Api").FullName);

            Container = builder.Build();

            this.Mediator = Container.Resolve<IMediator>();
            this.DbContext = Container.Resolve<ArchitectureProjectDbContext>();

            EnsureDatabase();

            _checkpoint = new Checkpoint
            {
                TablesToIgnore = new[] { "__EFMigrationsHistory" }
            };
        }

        [TearDown]
        public async Task Cleanup()
        {
            await ResetState();
            Container.Dispose();
        }

        private void EnsureDatabase()
        {
            DbContext.Database.EnsureCreated();

            if (DbContext.Database.IsSqlServer())
            {
                DbContext.Database.Migrate();
            }
        }

        public async Task ResetState()
        {
            await _checkpoint.Reset(Configuration.GetConnectionString("DefaultConnection"));
            _currentUserId = null;
        }
    }
}