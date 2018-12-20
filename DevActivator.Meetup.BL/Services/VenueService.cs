using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevActivator.Meetup.BL.Entities;
using DevActivator.Meetup.BL.Extensions;
using DevActivator.Meetup.BL.Interfaces;
using DevActivator.Meetup.BL.Models;

namespace DevActivator.Meetup.BL.Services
{
    public class VenueService : IVenueService
    {
        private readonly IVenueProvider _venueProvider;

        public VenueService(IVenueProvider venueProvider)
        {
            _venueProvider = venueProvider;
        }

        public async Task<List<AutocompleteRow>> GetAllVenuesAsync()
        {
            var venues = await _venueProvider.GetAllVenuesAsync().ConfigureAwait(false);
            return venues
                .Select(x => new AutocompleteRow {Id = x.Id, Name = x.Name})
                .ToList();
        }

        public async Task<VenueVm> GetVenueAsync(string venueId)
        {
            var venue = await _venueProvider.GetVenueOrDefaultAsync(venueId).ConfigureAwait(false);
            return venue.ToVm();
        }

        public async Task<VenueVm> AddVenueAsync(VenueVm venue)
        {
            venue.EnsureIsValid();
            
            var original = await _venueProvider.GetVenueOrDefaultAsync(venue.Id).ConfigureAwait(false);
            if (original != null)
            {
                throw new FormatException($"Данный {nameof(venue.Id)} \"{venue.Id}\" уже занят");
            }

            var entity = new Venue {Id = venue.Id}.Extend(venue);
            var res = await _venueProvider.SaveVenueAsync(entity).ConfigureAwait(false);
            return res.ToVm();
        }

        public async Task<VenueVm> UpdateVenueAsync(VenueVm venue)
        {
            venue.EnsureIsValid();
            var original = await _venueProvider.GetVenueOrDefaultAsync(venue.Id).ConfigureAwait(false);
            var res = await _venueProvider.SaveVenueAsync(original.Extend(venue)).ConfigureAwait(false);
            return res.ToVm();
        }
    }
}