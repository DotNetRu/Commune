using DotNetRuServer.Comon.BL.Caching;
using DotNetRuServer.Comon.BL.Config;
using DotNetRuServer.Meetups.BL.Interfaces;
using DotNetRuServer.Meetups.BL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetRuServer.Meetups.BL
{
    public static class MeetupServiceCollectionExtension
    {
        public static void AddMeetups<TSpeakerProvider, TTalkProvider, TVenueProvider, TFriendProvider, TMeetupProvider, TCommunityProvider, TImageProvider>(this IServiceCollection services, Settings settings)
            where TSpeakerProvider : class, ISpeakerProvider
            where TTalkProvider : class, ITalkProvider
            where TVenueProvider : class, IVenueProvider
            where TFriendProvider : class, IFriendProvider
            where TMeetupProvider : class, IMeetupProvider
            where TCommunityProvider : class, ICommunityProvider
            where TImageProvider : class, IImageProvider
        {
            services.AddSingleton(settings);

            services.AddSingleton<ISpeakerProvider, TSpeakerProvider>();
            services.AddSingleton<SpeakerService>();
            services.AddSingleton<ISpeakerService>(provider => 
                new CachedSpeakerService(provider.GetRequiredService<ICache>(), provider.GetRequiredService<SpeakerService>()));
            
            services.AddSingleton<ITalkProvider, TTalkProvider>();
            services.AddSingleton<TalkService>();
            services.AddSingleton<ITalkService>(provider => 
                new CachedTalkService(provider.GetRequiredService<ICache>(), provider.GetRequiredService<TalkService>()));
            
            services.AddSingleton<IVenueProvider, TVenueProvider>();
            services.AddSingleton<VenueService>();
            services.AddSingleton<IVenueService>(provider => 
                new CachedVenueService(provider.GetRequiredService<ICache>(), provider.GetRequiredService<VenueService>()));
            
            services.AddSingleton<IFriendProvider, TFriendProvider>();
            services.AddSingleton<FriendService>();
            services.AddSingleton<IFriendService>(provider => 
                new CachedFriendService(provider.GetRequiredService<ICache>(), provider.GetRequiredService<FriendService>()));
            
            services.AddSingleton<IMeetupProvider, TMeetupProvider>();
            services.AddSingleton<MeetupService>();
            services.AddSingleton<IMeetupService>(provider => 
                new CachedMeetupService(provider.GetRequiredService<ICache>(), provider.GetRequiredService<MeetupService>()));

            services.AddSingleton<IImageProvider, TImageProvider>();
            services.AddSingleton<IImageService, ImageService>();
            
            services.AddSingleton<ICommunityProvider, TCommunityProvider>();
            services.AddSingleton<ICommunityService, CommunityService>();
        }
    }
}