using System.Threading.Tasks;
using DotNetRuServer.Meetups.BL.Models;

namespace DotNetRuServer.Meetups.BL.Interfaces
{
    public interface IImageService
    {
        Task<ImageVm> GetFriendLogoOrDefaultAsync(string friendId, ImageSize size);

        Task<ImageVm> GetSpeakerAvatarOrDefaultAsync(string speakerId, ImageSize size);
    }
}