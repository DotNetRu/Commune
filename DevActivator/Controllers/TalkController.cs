using System.Collections.Generic;
using System.Threading.Tasks;
using DevActivator.Meetups.BL.Interfaces;
using DevActivator.Meetups.BL.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevActivator.Controllers
{
    [Route("api/[controller]")]
    public class TalkController : Controller
    {
        private readonly ITalkService _talkService;

        public TalkController(ITalkService talkService)
        {
            _talkService = talkService;
        }

        [HttpGet("[action]")]
        public Task<List<AutocompleteRow>> GetTalks()
            => _talkService.GetAllTalksAsync();

        [HttpGet("[action]/{talkId}")]
        public Task<TalkVm> GetTalk(string talkId)
            => _talkService.GetTalkAsync(talkId);

        [HttpPost("[action]")]
        public Task<TalkVm> AddTalk([FromBody] TalkVm talk)
            => _talkService.AddTalkAsync(talk);

        [HttpPost("[action]")]
        public Task<TalkVm> UpdateTalk([FromBody] TalkVm talk)
            => _talkService.UpdateTalkAsync(talk);
    }
}