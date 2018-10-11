using System.Threading.Tasks;
using DotNetRu.MeetupManagement.Domain.SocialNetworks;

namespace DotNetRu.MeetupManagement.Application.Services
{
    public class SocialIntegrationService : ISocialIntegrationService
    {
        private readonly ITelegramGateway _telegramGateway;

        public SocialIntegrationService(ITelegramGateway telegramGateway)
        {
            _telegramGateway = telegramGateway;
        }

        public Task SendText(string text)
        {
            return _telegramGateway.SendMessage(text);
        }
    }
}