using System.Threading.Tasks;

namespace DotNetRu.MeetupManagement.Application.Services
{
    public interface ISocialIntegrationService
    {
        Task SendText(string text);
    }
}