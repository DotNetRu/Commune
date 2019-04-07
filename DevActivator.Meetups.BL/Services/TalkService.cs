using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevActivator.Meetups.BL.Entities;
using DevActivator.Meetups.BL.Extensions;
using DevActivator.Meetups.BL.Interfaces;
using DevActivator.Meetups.BL.Models;

namespace DevActivator.Meetups.BL.Services
{
    public class TalkService : ITalkService
    {
        private readonly ITalkProvider _talkProvider;
        private readonly ISpeakerProvider _speakerProvider;

        public TalkService(ITalkProvider talkProvider, ISpeakerProvider speakerProvider)
        {
            _talkProvider = talkProvider;
            _speakerProvider = speakerProvider;
        }

        public async Task<List<AutocompleteRow>> GetAllTalksAsync()
        {
            var talks = await _talkProvider.GetAllTalksAsync().ConfigureAwait(false);
            return talks
                .Select(x => new AutocompleteRow {Id = x.ExportId, Name = x.Title})
                .ToList();
        }

        public async Task<TalkVm> GetTalkAsync(string talkId)
        {
            var talk = await _talkProvider.GetTalkOrDefaultExtendedAsync(talkId).ConfigureAwait(false);
            return talk.ToVm();
        }

        public async Task<TalkVm> AddTalkAsync(TalkVm talk)
        {
            talk.EnsureIsValid();
            var original = await _talkProvider.GetTalkOrDefaultAsync(talk.Id).ConfigureAwait(false);
            if (original != null)
            {
                throw new FormatException($"Данный {nameof(talk.Id)} \"{talk.Id}\" уже занят");
            }

            var speakers = await _speakerProvider.GetSpeakersByIdsAsync(talk.SpeakerIds);
            var entity = new Talk {ExportId = talk.Id, Speakers = new List<SpeakerTalk>()}.Extend(talk);
            foreach (var speaker in speakers)
            {
                entity.Speakers.Add(new SpeakerTalk
                {
                    Speaker = speaker,
                    Talk = entity
                });
            }


            var res = await _talkProvider.SaveTalkAsync(entity).ConfigureAwait(false);
            return res.ToVm();
        }

        public async Task<TalkVm> UpdateTalkAsync(TalkVm talk)
        {
            talk.EnsureIsValid();
            var original = await _talkProvider.GetTalkOrDefaultAsync(talk.Id).ConfigureAwait(false);
            var res = await _talkProvider.SaveTalkAsync(original.Extend(talk)).ConfigureAwait(false);
            return res.ToVm();
        }
    }
}