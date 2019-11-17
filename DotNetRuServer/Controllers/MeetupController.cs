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
    public class MeetupController : BaseController
    {
        private readonly IMeetupService _meetupService;

        protected MeetupController() { }

        public MeetupController(IMeetupService meetupService, ILoggerFactory logger) : base(logger)
        {
            _meetupService = meetupService;
        }

        [HttpGet("[action]")]
        public Task<List<AutocompleteRow>> GetMeetups()
        {
            try
            {
                LogMethodBegin();

                Task<List<AutocompleteRow>> result = _meetupService.GetAllMeetupsAsync();

                LogMethodEnd(result);

                return result;
            }
            catch (Exception e)
            {
                LogMethodError(e);
                throw;
            }
        } 

        [HttpGet("[action]/{meetupId}")]
        public Task<MeetupVm> GetMeetup(string meetupId)
        {
            try
            {
                LogMethodBegin(meetupId);

                Task<MeetupVm> result = _meetupService.GetMeetupAsync(meetupId);

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
        public Task<MeetupVm> AddMeetup([FromBody] MeetupVm meetup)
        {
            try
            {
                LogMethodBegin(meetup);

                Task<MeetupVm> result = _meetupService.AddMeetupAsync(meetup);

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
        public Task<MeetupVm> UpdateMeetup([FromBody] MeetupVm meetup)
        {
            try
            {
                LogMethodBegin(meetup);

                Task<MeetupVm> result = _meetupService.UpdateMeetupAsync(meetup);

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