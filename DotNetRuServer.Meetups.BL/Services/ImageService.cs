using System.IO;
using System.Threading.Tasks;
using DotNetRuServer.Meetups.BL.Entities;
using DotNetRuServer.Meetups.BL.Extensions;
using DotNetRuServer.Meetups.BL.Interfaces;
using DotNetRuServer.Meetups.BL.Models;
using SixLabors.ImageSharp;

namespace DotNetRuServer.Meetups.BL.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageProvider _imageProvider;
        private readonly IFriendProvider _friendProvider;
        private readonly ISpeakerProvider _speakerProvider;
        private readonly IUnitOfWork _unitOfWork;

        public ImageService(IImageProvider imageProvider, IFriendProvider friendProvider, ISpeakerProvider speakerProvider, IUnitOfWork unitOfWork)
        {
            _imageProvider = imageProvider;
            _friendProvider = friendProvider;
            _speakerProvider = speakerProvider;
            _unitOfWork = unitOfWork;
        }

        public async Task<ImageVm> GetFriendLogoOrDefaultAsync(string friendId, ImageSize size)
        {
            var friend = await _friendProvider.GetFriendOrDefaultAsync(friendId).ConfigureAwait(false);

            if (friend == null)
            {
                return null;
            }

            var imageUrl = size == ImageSize.Full ? friend.LogoId : friend.SmallLogoId;

            var image = await _imageProvider.GetImageOrDefaultAsync(imageUrl);

            return image?.ToVm();
        }

        public async Task<ImageVm> GetSpeakerAvatarOrDefaultAsync(string speakerId, ImageSize size)
        {
            var speaker = await _speakerProvider.GetSpeakerOrDefaultAsync(speakerId).ConfigureAwait(false);

            if (speaker == null)
            {
                return null;
            }

            var imageId = size == ImageSize.Full ? speaker.AvatarId : speaker.AvatarSmallId;

            var image = await _imageProvider.GetImageOrDefaultAsync(imageId);

            return image?.ToVm();
        }

        public async Task StoreSpeakerAvatarAsync(string speakerId, UploadImageInfo imageInfo, Stream imageStream)
        {
            var speaker = await _speakerProvider.GetSpeakerOrDefaultAsync(speakerId).ConfigureAwait(false);

            var imageId = await SaveImageDataAsync(imageInfo, imageStream);

            if (imageInfo.ImageSize == ImageSize.Full)
            {
                speaker.AvatarId = imageId;
            }
            else
            {
                speaker.AvatarSmallId = imageId;
            }

            await _speakerProvider.SaveSpeakerAsync(speaker);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task StoreFriendLogoAsync(string friendId, UploadImageInfo imageInfo, Stream imageStream)
        {
            var friend = await _friendProvider.GetFriendOrDefaultAsync(friendId).ConfigureAwait(false);

            var imageId = await SaveImageDataAsync(imageInfo, imageStream);

            if (imageInfo.ImageSize == ImageSize.Full)
            {
                friend.LogoId = imageId;
            }
            else
            {
                friend.SmallLogoId = imageId;
            }

            await _friendProvider.SaveFriendAsync(friend);

            await _unitOfWork.SaveChangesAsync();
        }

        private async Task<int> SaveImageDataAsync(UploadImageInfo info, Stream imageStream)
        {
            using (var memory = new MemoryStream())
            {
                await imageStream.CopyToAsync(memory);

                memory.Position = 0;

                var bytes = memory.ToArray();

                var image = Image.Load(bytes);

                var imageData = await _imageProvider.SaveImageAsync(new ImageData
                {
                    Data = bytes,
                    Height = image.Height,
                    Width = image.Width,
                    MimeType = info.MimeType,
                    IsSmall = info.ImageSize == ImageSize.Small
                });

                return imageData.Id;
            }


        }
    }
}