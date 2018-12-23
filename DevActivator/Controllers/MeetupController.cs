using System.Collections.Generic;
using System.Threading.Tasks;
using DevActivator.Meetups.BL.Interfaces;
using DevActivator.Meetups.BL.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevActivator.Controllers
{
    [Route("api/[controller]")]
    public class MeetupController : Controller
    {
        private readonly IMeetupService _meetupService;

        public MeetupController(IMeetupService meetupService)
        {
            _meetupService = meetupService;
        }

        [HttpGet("[action]")]
        public Task<List<AutocompleteRow>> GetMeetups()
            => _meetupService.GetAllMeetupsAsync();

        [HttpGet("[action]/{meetupId}")]
        public Task<MeetupVm> GetMeetup(string meetupId)
            => _meetupService.GetMeetupAsync(meetupId);

        [HttpPost("[action]")]
        public Task<MeetupVm> AddMeetup([FromBody] MeetupVm meetup)
            => _meetupService.AddMeetupAsync(meetup);

        [HttpPost("[action]")]
        public Task<MeetupVm> UpdateMeetup([FromBody] MeetupVm meetup)
            => _meetupService.UpdateMeetupAsync(meetup);
    }
}