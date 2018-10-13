using Autofac;
using DotNetRu.MeetupManagement.Infrastructure.EFCore;
using DotNetRu.MeetupManagement.Infrastructure.Telegram;

namespace DotNetRu.MeetupManagement.Infrastructure.DependencyInjection
{
    public class DataLayerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TalkDraftRepository>().AsImplementedInterfaces();
            builder.RegisterType<TalkRehearsalRepository>().AsImplementedInterfaces();
            builder.RegisterType<TelegramGateway>().AsImplementedInterfaces();
        }
    }
}
