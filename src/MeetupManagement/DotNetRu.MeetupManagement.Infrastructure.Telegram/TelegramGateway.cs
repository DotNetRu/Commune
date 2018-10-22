using System.Threading.Tasks;
using Telegram.Bot;
using DotNetRu.MeetupManagement.Domain.SocialNetworks;
using Microsoft.Extensions.Options;

namespace DotNetRu.MeetupManagement.Infrastructure.Telegram
{
    public class TelegramGateway : ITelegramGateway
    {
        private readonly IOptions<TelegramGatewaySettings> _settings;

        public TelegramGateway(IOptions<TelegramGatewaySettings> settings)
        {
            _settings = settings;
        }

        public async Task SendMessage(string text)
        {
            var botClient = new TelegramBotClient(_settings.Value.AuthToken);
            await botClient.SendTextMessageAsync(_settings.Value.ChannelName, text).ConfigureAwait(false);
        }
    }
}