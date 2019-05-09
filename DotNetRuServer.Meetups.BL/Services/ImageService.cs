using System.Threading.Tasks;
using DotNetRuServer.Meetups.BL.Extensions;
using DotNetRuServer.Meetups.BL.Interfaces;
using DotNetRuServer.Meetups.BL.Models;

namespace DotNetRuServer.Meetups.BL.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageProvider _imageProvider;
        private readonly IFriendProvider _friendProvider;
        private readonly ISpeakerProvider _speakerProvider;

        public ImageService(IImageProvider imageProvider, IFriendProvider friendProvider, ISpeakerProvider speakerProvider)
        {
            _imageProvider = imageProvider;
            _friendProvider = friendProvider;
            _speakerProvider = speakerProvider;
        }

        public async Task<ImageVm> GetFriendLogoOrDefaultAsync(string friendId, ImageSize size)
        {
            var friend = await _friendProvider.GetFriendOrDefaultAsync(friendId).ConfigureAwait(false);

            if (friend == null)
            {
                return null;
            }

            var imageUrl = size == ImageSize.Full ? friend.LogoUrl : friend.SmallLogoUrl;

            var image = await _imageProvider.GetImageOrDefault(imageUrl);
            if (image == null)
            {
                return null;
            }

            return image.ToVm();
        }

        public async Task<ImageVm> GetSpeakerAvatarOrDefaultAsync(string speakerId, ImageSize size)
        {
            var speaker = await _speakerProvider.GetSpeakerOrDefaultAsync(speakerId).ConfigureAwait(false);

            if (speaker == null)
            {
                return null;
            }

            var imageUrl = size == ImageSize.Full ? speaker.AvatarUrl : speaker.AvatarSmallUrl;

            var image = await _imageProvider.GetImageOrDefault(imageUrl);
            if (image == null)
            {
                return null;
            }

            return image.ToVm();
        }
    }
}