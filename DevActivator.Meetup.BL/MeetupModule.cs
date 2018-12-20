using Autofac;
using DevActivator.Common.BL.Caching;
using DevActivator.Common.BL.Config;
using DevActivator.Meetup.BL.Interfaces;
using DevActivator.Meetup.BL.Services;

namespace DevActivator.Meetup.BL
{
    public class MeetupModule<TSpeakerProvider, TTalkProvider, TVenueProvider> : Module
        where TSpeakerProvider : ISpeakerProvider
        where TTalkProvider : ITalkProvider
        where TVenueProvider : IVenueProvider
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
        }
    }
}