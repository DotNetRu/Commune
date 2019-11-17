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
    public class VenueController : BaseController
    {
        private readonly IVenueService _venueService;

        public VenueController(IVenueService venueService, ILoggerFactory logger) : base(logger)
        {
            _venueService = venueService;
        }

        [HttpGet("[action]")]
        public Task<List<AutocompleteRow>> GetVenues()
        {
            try
            {
                LogMethodBegin();

                Task<List<AutocompleteRow>> result = _venueService.GetAllVenuesAsync();

                LogMethodEnd(result);

                return result;
            }
            catch (Exception e)
            {
                LogMethodError(e);
                throw;
            }
        }

        [HttpGet("[action]/{venueId}")]
        public Task<VenueVm> GetVenue(string venueId)
        {
            try
            {
                LogMethodBegin(venueId);

                Task<VenueVm> result = _venueService.GetVenueAsync(venueId);

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
        public Task<VenueVm> AddVenue([FromBody] VenueVm venue)
        {
            try
            {
                LogMethodBegin(venue);

                Task<VenueVm> result = _venueService.AddVenueAsync(venue);

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
        public Task<VenueVm> UpdateVenue([FromBody] VenueVm venue)
        {
            try
            {
                LogMethodBegin(venue);

                Task<VenueVm> result = _venueService.UpdateVenueAsync(venue);

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