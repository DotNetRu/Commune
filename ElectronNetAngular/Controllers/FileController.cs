using System;
using System.IO;
using System.Threading.Tasks;
using DevActivator.Common.BL.Config;
using DevActivator.Common.BL.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElectronNetAngular.Controllers
{
    [Route("api/[controller]")]
    public class FileController : Controller
    {
        private readonly Settings _settings;

        public FileController(Settings settings)
        {
            _settings = settings;
        }

        [HttpPut("[action]/{speakerId}")]
        public async Task StoreSpeakerAvatar([FromRoute] string speakerId, [FromForm] IFormFile formFile)
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

            var filePath = _settings.GetSpeakerAvatarFilePath(speakerId);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await formFile.CopyToAsync(stream).ConfigureAwait(true);
            }
        }
    }
}