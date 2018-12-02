using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevActivator.Common.BL.Caching;
using DevActivator.Meetup.BL.Interfaces;
using DevActivator.Meetup.BL.Models;

namespace DevActivator.Meetup.BL.Services
{
    public class CachedSpeakerService : ISpeakerService
    {
        private readonly ICache _cache;
        private readonly ISpeakerService _speakerService;

        public CachedSpeakerService(ICache cache, ISpeakerService speakerServiceImplementation)
        {
            _cache = cache;
            _speakerService = speakerServiceImplementation;
        }

        public Task<List<SpeakerRow>> GetAllSpeakersAsync()
            => _cache.GetOrCreateAsync(nameof(GetAllSpeakersAsync),
                cacheEntry =>
                {
                    cacheEntry.SlidingExpiration = TimeSpan.FromMinutes(10);
                    return _speakerService.GetAllSpeakersAsync();
                }
            );

        public Task<SpeakerVm> GetSpeakerAsync(string speakerId)
            => _speakerService.GetSpeakerAsync(speakerId);

        public async Task<SpeakerVm> AddSpeakerAsync(SpeakerVm speaker)
        {
            var result = await _speakerService.AddSpeakerAsync(speaker).ConfigureAwait(false);

            _cache.Remove(nameof(GetAllSpeakersAsync));
            if (_cache.TryGetValue<List<SpeakerRow>>(nameof(GetAllSpeakersAsync), out var speakers))
            {
                speakers.ForEach(x => _cache.Remove($"{nameof(GetSpeakerAsync)}_{x.Id}"));
            }

            return result;
        }

        public async Task<SpeakerVm> UpdateSpeakerAsync(SpeakerVm speaker)
        {
            var result = await _speakerService.UpdateSpeakerAsync(speaker).ConfigureAwait(false);

            _cache.Remove(nameof(GetAllSpeakersAsync));
            if (_cache.TryGetValue<List<SpeakerRow>>(nameof(GetAllSpeakersAsync), out var speakers))
            {
                speakers.ForEach(x => _cache.Remove($"{nameof(GetSpeakerAsync)}_{x.Id}"));
            }

            return result;
        }
    }
}