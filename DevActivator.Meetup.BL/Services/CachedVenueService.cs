using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevActivator.Common.BL.Caching;
using DevActivator.Meetup.BL.Interfaces;
using DevActivator.Meetup.BL.Models;

namespace DevActivator.Meetup.BL.Services
{
    public class CachedVenueService : IVenueService
    {
        private readonly ICache _cache;
        private readonly IVenueService _venueService;

        public CachedVenueService(ICache cache, IVenueService venueService)
        {
            _cache = cache;
            _venueService = venueService;
        }

        public Task<List<AutocompleteRow>> GetAllVenuesAsync()
            => _cache.GetOrCreateAsync(nameof(GetAllVenuesAsync),
                cacheEntry =>
                {
                    cacheEntry.SlidingExpiration = TimeSpan.FromMinutes(10);
                    return _venueService.GetAllVenuesAsync();
                }
            );

        public Task<VenueVm> GetVenueAsync(string venueId)
            => _venueService.GetVenueAsync(venueId);

        public async Task<VenueVm> AddVenueAsync(VenueVm venue)
        {
            var result = await _venueService.AddVenueAsync(venue).ConfigureAwait(false);

            _cache.Remove(nameof(GetAllVenuesAsync));

            return result;
        }

        public async Task<VenueVm> UpdateVenueAsync(VenueVm venue)
        {
            var result = await _venueService.UpdateVenueAsync(venue).ConfigureAwait(false);

            _cache.Remove(nameof(GetAllVenuesAsync));

            return result;
        }
    }
}