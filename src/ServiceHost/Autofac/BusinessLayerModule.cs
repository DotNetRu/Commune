namespace DotNetRu.ServiceHost.Autofac
{
    using DotNetRu.MeetupManagement.Application.Services;
    using global::Autofac;

    public class BusinessLayerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ISocialIntegrationService>().AsImplementedInterfaces();
        }
    }
}
