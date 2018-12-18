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
    public class TalkService : ITalkService
    {
        private readonly ITalkProvider _talkProvider;

        public TalkService(ITalkProvider talkProvider)
        {
            _talkProvider = talkProvider;
        }

        public async Task<List<AutocompleteRow>> GetAllTalksAsync()
        {
            var talks = await _talkProvider.GetAllTalksAsync().ConfigureAwait(false);
            return talks
                .Select(x => new AutocompleteRow {Id = x.Id, Name = x.Title})
                .ToList();
        }

        public async Task<TalkVm> GetTalkAsync(string talkId)
        {
            var talk = await _talkProvider.GetTalkOrDefaultAsync(talkId).ConfigureAwait(false);
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

            var entity = new Talk {Id = talk.Id}.Extend(talk);
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