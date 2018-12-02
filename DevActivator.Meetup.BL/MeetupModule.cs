using Autofac;
using DevActivator.Common.BL.Caching;
using DevActivator.Common.BL.Config;
using DevActivator.Meetup.BL.Interfaces;
using DevActivator.Meetup.BL.Services;

namespace DevActivator.Meetup.BL
{
    public class MeetupModule<T> : Module where T : ISpeakerProvider
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

            builder.RegisterType<T>().As<ISpeakerProvider>().SingleInstance();
            builder.RegisterType<SpeakerService>().Named<ISpeakerService>(PureImplementation);
            builder.RegisterDecorator<ISpeakerService>(
                    (c, inner) => new CachedSpeakerService(c.Resolve<ICache>(), inner), PureImplementation)
                .SingleInstance();
        }
    }
}