using Autofac;

namespace DotNetRu.MeetupManagement.Infrastructure.Messaging
{
    public class MessagingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EventBus>().AsImplementedInterfaces();
        }
    }
}