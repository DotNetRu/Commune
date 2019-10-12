using DotNetRuServer.Comon.BL.Caching;
using DotNetRuServer.Comon.BL.Config;
using DotNetRuServer.Meetups.BL.Interfaces;
using DotNetRuServer.Meetups.BL.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetRuServer.Meetups.BL
{
    public static class MeetupServiceCollectionExtension
    {
        public static void AddMeetups<TSpeakerProvider, TTalkProvider, TVenueProvider, TFriendProvider, TMeetupProvider, TCommunityProvider, TImageProvider>(this IServiceCollection services, IConfiguration configuration)
            where TSpeakerProvider : class, ISpeakerProvider
            where TTalkProvider : class, ITalkProvider
            where TVenueProvider : class, IVenueProvider
            where TFriendProvider : class, IFriendProvider
            where TMeetupProvider : class, IMeetupProvider
            where TCommunityProvider : class, ICommunityProvider
            where TImageProvider : class, IImageProvider
        {
            services.Configure<Settings>(configuration.GetSection("Settings"));

            services.AddScoped<ISpeakerProvider, TSpeakerProvider>();
            services.AddScoped<SpeakerService>();
            services.AddScoped<ISpeakerService>(provider => 
                new CachedSpeakerService(provider.GetRequiredService<ICache>(), provider.GetRequiredService<SpeakerService>()));
            
            services.AddScoped<ITalkProvider, TTalkProvider>();
            services.AddScoped<TalkService>();
            services.AddScoped<ITalkService>(provider => 
                new CachedTalkService(provider.GetRequiredService<ICache>(), provider.GetRequiredService<TalkService>()));
            
            services.AddScoped<IVenueProvider, TVenueProvider>();
            services.AddScoped<VenueService>();
            services.AddScoped<IVenueService>(provider => 
                new CachedVenueService(provider.GetRequiredService<ICache>(), provider.GetRequiredService<VenueService>()));
            
            services.AddScoped<IFriendProvider, TFriendProvider>();
            services.AddScoped<FriendService>();
            services.AddScoped<IFriendService>(provider => 
                new CachedFriendService(provider.GetRequiredService<ICache>(), provider.GetRequiredService<FriendService>()));
            
            services.AddScoped<IMeetupProvider, TMeetupProvider>();
            services.AddScoped<MeetupService>();
            services.AddScoped<IMeetupService>(provider => 
                new CachedMeetupService(provider.GetRequiredService<ICache>(), provider.GetRequiredService<MeetupService>()));

            services.AddScoped<IImageProvider, TImageProvider>();
            services.AddScoped<IImageService, ImageService>();
            
            services.AddScoped<ICommunityProvider, TCommunityProvider>();
            services.AddScoped<ICommunityService, CommunityService>();
        }
    }
}