using System;
using System.IO;
using System.Threading.Tasks;
using DotNetRuServer.Comon.BL.Config;
using DotNetRuServer.Meetups.BL.Interfaces;
using DotNetRuServer.Meetups.BL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetRuServer.Controllers
{
    [Route("api/[controller]")]
    public class FileController : Controller
    {
        private readonly Settings _settings;
        private readonly IImageService _imageService;

        public FileController(Settings settings, IImageService imageService)
        {
            _settings = settings;
            _imageService = imageService;
        }

        [HttpPut("[action]/{speakerId}")]
        public Task StoreFullSpeakerAvatar([FromRoute] string speakerId, [FromForm] IFormFile formFile)
        {
            return StoreImageAsync(ImageSize.Full, formFile, (imageInfo, stream) =>
                _imageService.StoreSpeakerAvatarAsync(speakerId, imageInfo, stream));
        }

        [HttpPut("[action]/{speakerId}")]
        public Task StoreSmallSpeakerAvatar([FromRoute] string speakerId, [FromForm] IFormFile formFile)
        {
            return StoreImageAsync(ImageSize.Small, formFile, (imageInfo, stream) =>
                _imageService.StoreSpeakerAvatarAsync(speakerId, imageInfo, stream));
        }

        [HttpPut("[action]/{friendId}")]
        public Task StoreFullFriendLogo([FromRoute] string friendId, [FromForm] IFormFile formFile)
        {
            return StoreImageAsync(ImageSize.Full, formFile, (imageInfo, stream) =>
                _imageService.StoreFriendLogoAsync(friendId, imageInfo, stream));
        }

        [HttpPut("[action]/{friendId}")]
        public Task StoreSmallFriendLogo([FromRoute] string friendId, [FromForm] IFormFile formFile)
        {
            return StoreImageAsync(ImageSize.Small, formFile, (imageInfo, stream) =>
                _imageService.StoreFriendLogoAsync(friendId, imageInfo, stream));
        }

        private async Task StoreImageAsync(ImageSize imageSize, IFormFile formFile, Func<UploadImageInfo, Stream, Task> saveImageAsync)
        {
            if (formFile == null || formFile.Length <= 0)
            {
                throw new ArgumentException("Can't read the file", nameof(formFile));
            }

            if (formFile.Length > _settings.AvatarMaxSize)
            {
                throw new ArgumentOutOfRangeException(nameof(formFile),
                    $"File size must be lower than {_settings.AvatarMaxSize.ToString()}");
            }

            using (var stream = formFile.OpenReadStream())
            {
                var imageInfo = new UploadImageInfo
                {
                    ImageSize = imageSize,
                    MimeType = formFile.ContentType
                };

                await saveImageAsync(imageInfo, stream);
            }
        }

    }
}