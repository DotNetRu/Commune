using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetRuServer.Meetups.BL.Interfaces;
using DotNetRuServer.Meetups.BL.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetRuServer.Controllers
{
    [Route("api/[controller]")]
    public class SpeakerController : Controller
    {
        private readonly ISpeakerService _speakerService;
        private readonly IImageService _imageService;

        public SpeakerController(ISpeakerService speakerService, IImageService imageService)
        {
            _speakerService = speakerService;
            _imageService = imageService;
        }

        [HttpGet("[action]")]
        public Task<List<AutocompleteRow>> GetSpeakers()
            => _speakerService.GetAllSpeakersAsync();

        [HttpGet("[action]/{speakerId}")]
        public Task<SpeakerVm> GetSpeaker(string speakerId)
            => _speakerService.GetSpeakerAsync(speakerId);

        [HttpPost("[action]")]
        public Task<SpeakerVm> AddSpeaker([FromBody] SpeakerVm speaker)
            => _speakerService.AddSpeakerAsync(speaker);

        [HttpPost("[action]")]
        public Task<SpeakerVm> UpdateSpeaker([FromBody] SpeakerVm speaker)
            => _speakerService.UpdateSpeakerAsync(speaker);

        [HttpGet("[action]/{speakerId}/fullAvatar")]
        public Task<ActionResult> GetFriendFullLogo(string speakerId)
            => GetAvatarAsync(speakerId, ImageSize.Full);

        [HttpGet("[action]/{speakerId}/avatar")]
        public Task<ActionResult> GetFriendSmallLogo(string speakerId)
            => GetAvatarAsync(speakerId, ImageSize.Small);

        private async Task<ActionResult> GetAvatarAsync(string speakerId, ImageSize imageSize)
        {
            var image = await _imageService.GetSpeakerAvatarOrDefaultAsync(speakerId, imageSize);
            if (image == null)
            {
                return NotFound();
            }

            return new FileContentResult(image.Data, image.MimeType);
        }
    }
}