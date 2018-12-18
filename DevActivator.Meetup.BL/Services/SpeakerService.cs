using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevActivator.Common.BL.Config;
using DevActivator.Meetup.BL.Entities;
using DevActivator.Meetup.BL.Extensions;
using DevActivator.Meetup.BL.Interfaces;
using DevActivator.Meetup.BL.Models;

namespace DevActivator.Meetup.BL.Services
{
    public class SpeakerService : ISpeakerService
    {
        private readonly Settings _settings;
        private readonly ISpeakerProvider _speakerProvider;

        public SpeakerService(Settings settings, ISpeakerProvider speakerProvider)
        {
            _settings = settings;
            _speakerProvider = speakerProvider;
        }

        public async Task<List<AutocompleteRow>> GetAllSpeakersAsync()
        {
            var speakers = await _speakerProvider.GetAllSpeakersAsync().ConfigureAwait(false);
            return speakers
                .Select(x => new AutocompleteRow {Id = x.Id, Name = x.Name})
                .ToList();
        }

        public async Task<SpeakerVm> GetSpeakerAsync(string speakerId)
        {
            var speaker = await _speakerProvider.GetSpeakerOrDefaultAsync(speakerId).ConfigureAwait(false);
            return speaker.ToVm(speaker.GetLastUpdateDate(_settings));
        }

        public async Task<SpeakerVm> AddSpeakerAsync(SpeakerVm speaker)
        {
            speaker.EnsureIsValid();

            var original = await _speakerProvider.GetSpeakerOrDefaultAsync(speaker.Id).ConfigureAwait(false);
            if (original != null)
            {
                throw new FormatException($"Данный {nameof(speaker.Id)} \"{speaker.Id}\" уже занят");
            }

            var entity = new Speaker {Id = speaker.Id}.Extend(speaker);
            var res = await _speakerProvider.SaveSpeakerAsync(entity).ConfigureAwait(false);
            return res.ToVm(res.GetLastUpdateDate(_settings));
        }

        public async Task<SpeakerVm> UpdateSpeakerAsync(SpeakerVm speaker)
        {
            speaker.EnsureIsValid();
            var original = await _speakerProvider.GetSpeakerOrDefaultAsync(speaker.Id).ConfigureAwait(false);
            var res = await _speakerProvider.SaveSpeakerAsync(original.Extend(speaker)).ConfigureAwait(false);
            return res.ToVm(res.GetLastUpdateDate(_settings));
        }
    }
}