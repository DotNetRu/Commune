using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetRuServer.Meetups.BL.Interfaces;
using DotNetRuServer.Meetups.BL.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevActivator.Controllers
{
    [Route("api/[controller]")]
    public class VenueController : Controller
    {
        private readonly IVenueService _venueService;

        public VenueController(IVenueService venueService)
        {
            _venueService = venueService;
        }

        [HttpGet("[action]")]
        public Task<List<AutocompleteRow>> GetVenues()
            => _venueService.GetAllVenuesAsync();

        [HttpGet("[action]/{venueId}")]
        public Task<VenueVm> GetVenue(string venueId)
            => _venueService.GetVenueAsync(venueId);

        [HttpPost("[action]")]
        public Task<VenueVm> AddVenue([FromBody] VenueVm venue)
            => _venueService.AddVenueAsync(venue);

        [HttpPost("[action]")]
        public Task<VenueVm> UpdateVenue([FromBody] VenueVm venue)
            => _venueService.UpdateVenueAsync(venue);
    }
}