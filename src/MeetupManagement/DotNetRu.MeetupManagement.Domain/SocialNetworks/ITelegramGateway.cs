using System.Threading.Tasks;

namespace DotNetRu.MeetupManagement.Domain.SocialNetworks
{
    public interface ITelegramGateway
    {
        Task SendMessage(string text);
    }
}