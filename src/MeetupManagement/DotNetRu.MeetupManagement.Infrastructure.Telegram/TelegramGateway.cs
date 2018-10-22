using System.Threading.Tasks;
using Telegram.Bot;
using DotNetRu.MeetupManagement.Domain.SocialNetworks;

namespace DotNetRu.MeetupManagement.Infrastructure.Telegram
{
    public class TelegramGateway : ITelegramGateway
    {
        private readonly TelegramGatewaySettings _settings;

        public TelegramGateway(TelegramGatewaySettings settings)
        {
            _settings = settings;
        }

        public async Task SendMessage(string text)
        {
            var botClient = new TelegramBotClient(_settings.AuthToken);
            await botClient.SendTextMessageAsync(_settings.ChannelName, text).ConfigureAwait(false);
        }
    }
}