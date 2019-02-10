namespace DotNetRu.ServiceHost.Autofac
{
    using MeetupManagement.Domain.Drafts;
    using global::Autofac;

    public class DomainLayerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TalkDraftService>().AsImplementedInterfaces();
        }
    }
}