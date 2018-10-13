namespace DotNetRu.ServiceHost.Autofac
{
    using DotNetRu.MeetupManagement.Infrastructure.Telegram;
    using global::Autofac;
    using Microsoft.Extensions.Configuration;

    public class ConfigurationModule : Module
    {
        private readonly IConfiguration _configuration;

        public ConfigurationModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register((arg) =>
            {
                var settings = new TelegramGatewaySettings();
                _configuration.GetSection("SocialNetworks").GetSection("Telegram").Bind(settings);
                return settings;
            }).SingleInstance();
        }
    }
}
