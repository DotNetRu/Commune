using Autofac;
using DevActivator.Common.BL.Caching;
using DevActivator.Common.BL.Config;
using DevActivator.Meetups.BL.Interfaces;
using DevActivator.Meetups.BL.Services;

namespace DevActivator.Meetups.BL
{
    public class MeetupModule<TSpeakerProvider, TTalkProvider, TVenueProvider, TFriendProvider, TMeetupProvider> : Module
        where TSpeakerProvider : ISpeakerProvider
        where TTalkProvider : ITalkProvider
        where TVenueProvider : IVenueProvider
        where TFriendProvider : IFriendProvider
        where TMeetupProvider : IMeetupProvider
    {
        private const string PureImplementation = nameof(PureImplementation);

        private readonly Settings _settings;

        public MeetupModule(Settings settings)
        {
            _settings = settings;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(x => _settings).AsSelf().SingleInstance();

            builder.RegisterType<TSpeakerProvider>().As<ISpeakerProvider>().SingleInstance();
            builder.RegisterType<SpeakerService>().Named<ISpeakerService>(PureImplementation);
            builder.RegisterDecorator<ISpeakerService>(
                    (c, inner) => new CachedSpeakerService(c.Resolve<ICache>(), inner), PureImplementation)
                .SingleInstance();

            builder.RegisterType<TTalkProvider>().As<ITalkProvider>().SingleInstance();
            builder.RegisterType<TalkService>().Named<ITalkService>(PureImplementation);
            builder.RegisterDecorator<ITalkService>(
                    (c, inner) => new CachedTalkService(c.Resolve<ICache>(), inner), PureImplementation)
                .SingleInstance();

            builder.RegisterType<TVenueProvider>().As<IVenueProvider>().SingleInstance();
            builder.RegisterType<VenueService>().Named<IVenueService>(PureImplementation);
            builder.RegisterDecorator<IVenueService>(
                    (c, inner) => new CachedVenueService(c.Resolve<ICache>(), inner), PureImplementation)
                .SingleInstance();

            builder.RegisterType<TFriendProvider>().As<IFriendProvider>().SingleInstance();
            builder.RegisterType<FriendService>().Named<IFriendService>(PureImplementation);
            builder.RegisterDecorator<IFriendService>(
                    (c, inner) => new CachedFriendService(c.Resolve<ICache>(), inner), PureImplementation)
                .SingleInstance();

            builder.RegisterType<TMeetupProvider>().As<IMeetupProvider>().SingleInstance();
            builder.RegisterType<MeetupService>().Named<IMeetupService>(PureImplementation);
            builder.RegisterDecorator<IMeetupService>(
                    (c, inner) => new CachedMeetupService(c.Resolve<ICache>(), inner), PureImplementation)
                .SingleInstance();
        }
    }
}