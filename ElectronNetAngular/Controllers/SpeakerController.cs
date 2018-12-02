using System.Collections.Generic;
using System.Threading.Tasks;
using DevActivator.Meetup.BL.Interfaces;
using DevActivator.Meetup.BL.Models;
using Microsoft.AspNetCore.Mvc;

namespace ElectronNetAngular.Controllers
{
    [Route("api/[controller]")]
    public class SpeakerController : Controller
    {
        private readonly ISpeakerService _speakerService;

        public SpeakerController(ISpeakerService speakerService)
        {
            _speakerService = speakerService;
        }

        [HttpGet("[action]")]
        public Task<List<SpeakerRow>> GetSpeakers()
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
    }
}