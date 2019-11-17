using System;
using System.Threading;
using System.Threading.Tasks;
using DotNetRuServer.Integration.TimePad;
using DotNetRuServer.Meetups.BL.Entities;
using DotNetRuServer.Meetups.BL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DotNetRuServer.Controllers
{
    [Route("api/[controller]")]
    public class TimePadController : BaseController
    {
        private readonly TimePadIntegrationService _timePadService;
        private readonly IMeetupProvider _meetupProvider;

        public TimePadController(TimePadIntegrationService timePadService, IMeetupProvider meetupProvider, ILoggerFactory logger) : base(logger)
        {
            _timePadService = timePadService;
            _meetupProvider = meetupProvider;
        }

        [HttpPost("[action]/{meetupId}")]
        public async Task Create(string meetupId, CancellationToken ct)
        {
            try
            {
                LogMethodBegin(meetupId);

                Meetup meetup = await _meetupProvider.GetMeetupOrDefaultExtendedAsync(meetupId);
                await _timePadService.CreateDraftEventAsync(meetup, ct);

                LogMethodEnd();
            }
            catch (Exception e)
            {
                LogMethodError(e);
                throw;
            }
        }
    }
}