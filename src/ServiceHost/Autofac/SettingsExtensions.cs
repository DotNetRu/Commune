using Microsoft.Extensions.DependencyInjection;

namespace DotNetRu.ServiceHost.Autofac
{
    using DotNetRu.MeetupManagement.Infrastructure.Telegram;
    using Microsoft.Extensions.Configuration;

    public static class SettingsExtensions
    {
        public static void ConfigureSettings(this IServiceCollection service, IConfiguration configuration)
        {
            service.Configure<TelegramGatewaySettings>(configuration.GetSection("SocialNetworks"));
        }
    }
}