using System.Collections.Generic;
using System.Threading.Tasks;
using DevActivator.Meetups.BL.Interfaces;
using DevActivator.Meetups.BL.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevActivator.Controllers
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
    }
}