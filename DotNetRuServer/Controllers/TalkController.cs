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
    public class TalkController : BaseController
    {
        private readonly ITalkService _talkService;

        protected TalkController() { }

        public TalkController(ITalkService talkService, ILoggerFactory logger) : base(logger)
        {
            _talkService = talkService;
        }

        [HttpGet("[action]")]
        public Task<List<AutocompleteRow>> GetTalks()
        {
            try
            {
                LogMethodBegin();

                Task<List<AutocompleteRow>> result = _talkService.GetAllTalksAsync();

                LogMethodEnd(result);

                return result;
            }
            catch (Exception e)
            {
                LogMethodError(e);
                throw;
            }
        }

        [HttpGet("[action]/{talkId}")]
        public Task<TalkVm> GetTalk(string talkId)
        {
            try
            {
                LogMethodBegin(talkId);

                Task<TalkVm> result = _talkService.GetTalkAsync(talkId);

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
        public Task<TalkVm> AddTalk([FromBody] TalkVm talk)
        {
            try
            {
                LogMethodBegin(talk);

                Task<TalkVm> result = _talkService.AddTalkAsync(talk);

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
        public Task<TalkVm> UpdateTalk([FromBody] TalkVm talk)
        {
            try
            {
                LogMethodBegin(talk);

                Task<TalkVm> result = _talkService.UpdateTalkAsync(talk);

                LogMethodEnd(result);

                return result;
            }
            catch (Exception e)
            {
                LogMethodError(e);
                throw;
            }
        }
    }
}