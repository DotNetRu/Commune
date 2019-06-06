using System.IO;
using System.Threading.Tasks;
using DotNetRuServer.Meetups.BL.Models;

namespace DotNetRuServer.Meetups.BL.Interfaces
{
    public interface IImageService
    {
        Task<ImageVm> GetFriendLogoOrDefaultAsync(string friendId, ImageSize size);

        Task<ImageVm> GetSpeakerAvatarOrDefaultAsync(string speakerId, ImageSize size);

        Task StoreSpeakerAvatarAsync(string speakerId, UploadImageInfo imageInfo, Stream imageStream);

        Task StoreFriendLogoAsync(string friendId, UploadImageInfo imageInfo, Stream imageStream);
    }
}