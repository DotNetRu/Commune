using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevActivator.Common.BL.Caching;
using DevActivator.Meetups.BL.Interfaces;
using DevActivator.Meetups.BL.Models;

namespace DevActivator.Meetups.BL.Services
{
    public class CachedMeetupService : IMeetupService
    {
        private readonly ICache _cache;
        private readonly IMeetupService _meetupService;

        public CachedMeetupService(ICache cache, IMeetupService meetupServiceImplementation)
        {
            _cache = cache;
            _meetupService = meetupServiceImplementation;
        }

        public Task<List<AutocompleteRow>> GetAllMeetupsAsync()
            => _cache.GetOrCreateAsync(nameof(GetAllMeetupsAsync),
                cacheEntry =>
                {
                    cacheEntry.SlidingExpiration = TimeSpan.FromMinutes(10);
                    return _meetupService.GetAllMeetupsAsync();
                }
            );

        public Task<MeetupVm> GetMeetupAsync(string meetupId)
            => _meetupService.GetMeetupAsync(meetupId);

        public async Task<MeetupVm> AddMeetupAsync(MeetupVm meetup)
        {
            var result = await _meetupService.AddMeetupAsync(meetup).ConfigureAwait(false);

            _cache.Remove(nameof(GetAllMeetupsAsync));

            return result;
        }

        public async Task<MeetupVm> UpdateMeetupAsync(MeetupVm meetup)
        {
            var result = await _meetupService.UpdateMeetupAsync(meetup).ConfigureAwait(false);

            _cache.Remove(nameof(GetAllMeetupsAsync));
            if (_cache.TryGetValue<List<AutocompleteRow>>(nameof(GetAllMeetupsAsync), out var meetups))
            {
                meetups.ForEach(x => _cache.Remove($"{nameof(GetMeetupAsync)}_{x.Id}"));
            }

            return result;
        }
    }
}