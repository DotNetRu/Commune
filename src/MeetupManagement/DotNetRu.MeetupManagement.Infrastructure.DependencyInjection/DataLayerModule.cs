using Autofac;
using DotNetRu.MeetupManagement.Domain.SocialNetworks;
using DotNetRu.MeetupManagement.Infrastructure.EFCore;

namespace DotNetRu.MeetupManagement.Infrastructure.DependencyInjection
{
    public class DataLayerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TalkDraftRepository>().AsImplementedInterfaces();
            builder.RegisterType<TalkRehearsalRepository>().AsImplementedInterfaces();
            builder.RegisterType<ITelegramGateway>().AsImplementedInterfaces();
        }
    }
}
