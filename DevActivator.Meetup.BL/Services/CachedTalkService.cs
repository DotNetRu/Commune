using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevActivator.Common.BL.Caching;
using DevActivator.Meetup.BL.Interfaces;
using DevActivator.Meetup.BL.Models;

namespace DevActivator.Meetup.BL.Services
{
    public class CachedTalkService : ITalkService
    {
        private readonly ICache _cache;
        private readonly ITalkService _talkService;

        public CachedTalkService(ICache cache, ITalkService talkService)
        {
            _cache = cache;
            _talkService = talkService;
        }

        public Task<List<AutocompleteRow>> GetAllTalksAsync()
            => _cache.GetOrCreateAsync(nameof(GetAllTalksAsync),
                cacheEntry =>
                {
                    cacheEntry.SlidingExpiration = TimeSpan.FromMinutes(10);
                    return _talkService.GetAllTalksAsync();
                }
            );

        public Task<TalkVm> GetTalkAsync(string talkId)
            => _talkService.GetTalkAsync(talkId);

        public async Task<TalkVm> AddTalkAsync(TalkVm talk)
        {
            var result = await _talkService.AddTalkAsync(talk).ConfigureAwait(false);

            _cache.Remove(nameof(GetAllTalksAsync));

            return result;
        }

        public async Task<TalkVm> UpdateTalkAsync(TalkVm talk)
        {
            var result = await _talkService.UpdateTalkAsync(talk).ConfigureAwait(false);

            _cache.Remove(nameof(GetAllTalksAsync));

            return result;
        }
    }
}