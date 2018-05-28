using Xunit;
using Autofac;
using System.Linq;
using Autofac.Core;
using DotNetRu.MeetupManagement.Core;
using DotNetRu.MeetupManagement.Infrastructure.EFCore;
using DotNetRu.MeetupManagement.Infrastructure.Messaging;

namespace DotNetRu.ServiceHost.Tests.AutofacModules
{
    public class AutofacModulesTest
    {
        [Fact]
        public void AllComponentsRegisteredInModuleMustBeResolved()
        {
            ResolveComponents(new MessagingModule(), new EFCoreModule(), new CoreModule());
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
                        var instance = container.Resolve(service.ServiceType);
                    }
                }
            }

        }
    }
}
