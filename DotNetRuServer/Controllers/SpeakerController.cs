using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetRuServer.Meetups.BL.Interfaces;
using DotNetRuServer.Meetups.BL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DotNetRuServer.Controllers
{
    [Route("api/[controller]")]
    public class SpeakerController : BaseController
    {
        private readonly ISpeakerService _speakerService;
        private readonly IImageService _imageService;

        protected SpeakerController() { }

        public SpeakerController(ISpeakerService speakerService, IImageService imageService, ILoggerFactory logger) : base(logger)
        {
            _speakerService = speakerService;
            _imageService = imageService;
        }

        [HttpGet("[action]")]
        public Task<List<AutocompleteRow>> GetSpeakers()
        {
            try
            {
                LogMethodBegin();

                Task<List<AutocompleteRow>> result = _speakerService.GetAllSpeakersAsync();

                LogMethodEnd(result);

                return result;
            }
            catch (Exception e)
            {
                LogMethodError(e);
                throw;
            }
        }

        [HttpGet("[action]/{speakerId}")]
        public Task<SpeakerVm> GetSpeaker(string speakerId)
        {
            try
            {
                LogMethodBegin(speakerId);

                Task<SpeakerVm> result = _speakerService.GetSpeakerAsync(speakerId);

                LogMethodEnd(result);

                return result;
            }
            catch (Exception e)
            {
                LogMethodError(e);
                throw;
            }
        }

        [HttpPost("[action]")]
        public Task<SpeakerVm> AddSpeaker([FromBody] SpeakerVm speaker)
        {
            try
            {
                LogMethodBegin(speaker);

                Task<SpeakerVm> result = _speakerService.AddSpeakerAsync(speaker);

                LogMethodEnd(result);

                return result;
            }
            catch (Exception e)
            {
                LogMethodError(e);
                throw;
            }
        }

        [HttpPost("[action]")]
        public Task<SpeakerVm> UpdateSpeaker([FromBody] SpeakerVm speaker)
        {
            try
            {
                LogMethodBegin(speaker);

                Task<SpeakerVm> result = _speakerService.UpdateSpeakerAsync(speaker);

                LogMethodEnd(result);

                return result;
            }
            catch (Exception e)
            {
                LogMethodError(e);
                throw;
            }
        }

        [HttpGet("[action]/{speakerId}/fullAvatar")]
        public Task<ActionResult> GetFriendFullLogo(string speakerId)
        {
            try
            {
                LogMethodBegin(speakerId);

                Task<ActionResult> result = GetAvatarAsync(speakerId, ImageSize.Full);

                LogMethodEnd(result);

                return result;
            }
            catch (Exception e)
            {
                LogMethodError(e);
                throw;
            }
        }

        [HttpGet("[action]/{speakerId}/avatar")]
        public Task<ActionResult> GetFriendSmallLogo(string speakerId)
        {
            try
            {
                LogMethodBegin(speakerId);

                Task<ActionResult> result = GetAvatarAsync(speakerId, ImageSize.Small);

                LogMethodEnd(result);

                return result;
            }
            catch (Exception e)
            {
                LogMethodError(e);
                throw;
            }
        }

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