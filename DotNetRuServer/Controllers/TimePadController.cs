using System.Threading;
using System.Threading.Tasks;
using DotNetRuServer.Integration.TimePad;
using DotNetRuServer.Meetups.BL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DotNetRuServer.Controllers
{
    [Route("api/[controller]")]
    public class TimePadController : Controller
    {
        private readonly TimePadIntegrationService _timePadService;
        private readonly IMeetupProvider _meetupProvider;

        public TimePadController(TimePadIntegrationService timePadService, IMeetupProvider meetupProvider)
        {
            _timePadService = timePadService;
            _meetupProvider = meetupProvider;
        }

        [HttpGet("[action]/{meetupId}")]
        public async Task Create(string meetupId, CancellationToken ct)
        {
            var meetup = await _meetupProvider.GetMeetupOrDefaultExtendedAsync(meetupId);
            await _timePadService.CreateDraftEventAsync(meetup, ct);
        }
    }
}