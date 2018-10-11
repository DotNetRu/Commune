using Telegram.Bot;
using System.Threading.Tasks;
using DotNetRu.MeetupManagement.Domain.SocialNetworks;

namespace DotNetRu.MeetupManagement.Infrastructure.Telegram
{
    public class TelegramGateway : ITelegramGateway
    {
        private readonly string _authToken;
        private readonly string _channel;

        public TelegramGateway(string authToken, string channel)
        {
            _authToken = authToken;
            _channel = channel;
        }

        public async Task SendMessage(string text)
        {
            var botClient = new TelegramBotClient(_authToken);
            await botClient.SendTextMessageAsync(_channel, text);
        }
    }
}