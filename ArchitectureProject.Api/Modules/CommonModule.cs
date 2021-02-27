using System.Reflection;
using ArchitectureProject.Infrastructure.Services;
using Autofac;
using MediatR.Extensions.Autofac.DependencyInjection;
using Module = Autofac.Module;

namespace ArchitectureProject.Api.Modules
{
    public class CommonModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.AddMediatR(
                Assembly.GetCallingAssembly(),
                Assembly.GetExecutingAssembly(),
                Assembly.Load("ArchitectureProject.Logic"));

            builder.RegisterAssemblyTypes(Assembly.Load("ArchitectureProject.Infrastructure"))
                .AsImplementedInterfaces();

            builder.RegisterType<PresenceTrackerService>()
                .SingleInstance();
        }
    }
}
