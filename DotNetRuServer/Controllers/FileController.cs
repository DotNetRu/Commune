using System;
using System.IO;
using System.Threading.Tasks;
using DotNetRuServer.Comon.BL.Config;
using DotNetRuServer.Meetups.BL.Interfaces;
using DotNetRuServer.Meetups.BL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DotNetRuServer.Controllers
{
    [Route("api/[controller]")]
    public class FileController : BaseController
    {
        private readonly Settings _settings;
        private readonly IImageService _imageService;

        public FileController(IOptionsMonitor<Settings> settingsAccessor, IImageService imageService, ILoggerFactory logger) : base(logger)
        {
            _settings = settingsAccessor.CurrentValue;
            _imageService = imageService;
        }

        [HttpPut("[action]/{speakerId}")]
        public Task StoreFullSpeakerAvatar([FromRoute] string speakerId, [FromForm] IFormFile formFile)
        {
            try
            {
                LogMethodBegin(formFile);

                Task result = StoreImageAsync(ImageSize.Full, formFile, (imageInfo, stream) =>
                    _imageService.StoreSpeakerAvatarAsync(speakerId, imageInfo, stream));
                
                LogMethodEnd(result);

                return result;
            }
            catch (Exception e)
            {
                LogMethodError(e);
                throw;
            }

            
        }

        [HttpPut("[action]/{speakerId}")]
        public Task StoreSmallSpeakerAvatar([FromRoute] string speakerId, [FromForm] IFormFile formFile)
        {
            try
            {
                LogMethodBegin(formFile);

                Task result = StoreImageAsync(ImageSize.Small, formFile, (imageInfo, stream) =>
                    _imageService.StoreSpeakerAvatarAsync(speakerId, imageInfo, stream));

                LogMethodEnd(result);

                return result;
            }
            catch (Exception e)
            {
                LogMethodError(e);
                throw;
            }
        }

        [HttpPut("[action]/{friendId}")]
        public Task StoreFullFriendLogo([FromRoute] string friendId, [FromForm] IFormFile formFile)
        {
            try
            {
                LogMethodBegin(formFile);

                Task result = StoreImageAsync(ImageSize.Full, formFile, (imageInfo, stream) =>
                    _imageService.StoreFriendLogoAsync(friendId, imageInfo, stream));

                LogMethodEnd(result);

                return result;
            }
            catch (Exception e)
            {
                LogMethodError(e);
                throw;
            }
        }

        [HttpPut("[action]/{friendId}")]
        public Task StoreSmallFriendLogo([FromRoute] string friendId, [FromForm] IFormFile formFile)
        {
            try
            {
                LogMethodBegin(formFile);

                Task result = StoreImageAsync(ImageSize.Small, formFile, (imageInfo, stream) =>
                    _imageService.StoreFriendLogoAsync(friendId, imageInfo, stream));

                LogMethodEnd(result);

                return result;
            }
            catch (Exception e)
            {
                LogMethodError(e);
                throw;
            }
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