using System.Linq;
using Xunit;
using Autofac;
using Autofac.Core;
using DotNetRu.MeetupManagement.Infrastructure.DependencyInjection;

namespace DotNetRu.ServiceHost.Tests.AutofacModules
{
    public class AutofacModulesTest
    {
        [Fact]
#pragma warning disable CA1822 // Mark members as static
        public void AllComponentsRegisteredInModuleMustBeResolved()
#pragma warning restore CA1822 // Mark members as static
        {
            ResolveComponents(new DataLayerModule());
        }

        private static void ResolveComponents(params Module[] modules)
        {
            var builder = new ContainerBuilder();
            foreach (var module in modules)
            {
                builder.RegisterModule(module);
            }

            using (var container = builder.Build())
            {
                var registrations = container.ComponentRegistry.Registrations.ToList();

                foreach (var registration in registrations)
                {
                    foreach (var service in registration.Services.OfType<TypedService>())
                    {
                        container.Resolve(service.ServiceType);
                    }
                }
            }
        }
    }
}
