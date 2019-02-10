using Autofac;
using DotNetRu.MeetupManagement.Application.Services;

namespace DotNetRu.ServiceHost.Autofac
{
    public class ApplicationLayerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TalkDraftService>().AsImplementedInterfaces();
        }
    }
}
