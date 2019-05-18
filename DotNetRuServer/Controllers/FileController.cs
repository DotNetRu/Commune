using System;
using System.IO;
using System.Threading.Tasks;
using DotNetRuServer.Comon.BL.Config;
using DotNetRuServer.Comon.BL.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetRuServer.Controllers
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
            throw new NotImplementedException();
        }

        [HttpPut("[action]/{friendId}")]
        public async Task StoreFriendAvatar([FromRoute] string friendId, [FromForm] IFormFile formFile)
        {
            throw new NotImplementedException();
        }
    }
}