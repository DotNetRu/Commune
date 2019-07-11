using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetRuServer.Meetups.BL.Entities;
using DotNetRuServer.Meetups.BL.Extensions;
using DotNetRuServer.Meetups.BL.Interfaces;
using DotNetRuServer.Meetups.BL.Models;

namespace DotNetRuServer.Meetups.BL.Services
{
    public class TalkService : ITalkService
    {
        private readonly ISpeakerProvider _speakerProvider;
        private readonly ITalkProvider _talkProvider;
        private readonly IUnitOfWork _unitOfWork;

        public TalkService(ITalkProvider talkProvider, ISpeakerProvider speakerProvider, IUnitOfWork unitOfWork)
        {
            _talkProvider = talkProvider;
            _speakerProvider = speakerProvider;
            _unitOfWork = unitOfWork;
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
            if (original != null) throw new FormatException($"Данный {nameof(talk.Id)} \"{talk.Id}\" уже занят");

            var speakers =
                await _speakerProvider.GetSpeakersByIdsAsync(talk.SpeakerIds.Select(x => x.SpeakerId).ToList());
            var entity = new Talk {ExportId = talk.Id, Speakers = new List<SpeakerTalk>()}.Extend(talk);
            foreach (var speaker in speakers)
                entity.Speakers.Add(new SpeakerTalk
                {
                    Speaker = speaker,
                    Talk = entity
                });


            var res = await _talkProvider.SaveTalkAsync(entity).ConfigureAwait(false);
            return res.ToVm();
        }

        public async Task<TalkVm> UpdateTalkAsync(TalkVm talk)
        {
            talk.EnsureIsValid();

            var original = await _talkProvider.GetTalkOrDefaultExtendedAsync(talk.Id).ConfigureAwait(false);
            original.ExportId = talk.Id;
            original.Title = talk.Title;
            original.Description = talk.Description;
            original.CodeUrl = talk.CodeUrl;
            original.SlidesUrl = talk.SlidesUrl;
            original.VideoUrl = talk.VideoUrl;

            var speakers =
                await _speakerProvider.GetSpeakersByIdsAsync(talk.SpeakerIds.Select(x => x.SpeakerId).ToList());
            foreach (var oldSpeaker in original.Speakers) _talkProvider.RemoveSpeaker(original, oldSpeaker.SpeakerId);
            foreach (var speaker in speakers)
                original.Speakers.Add(new SpeakerTalk
                {
                    Speaker = speaker,
                    Talk = original
                });

            await _unitOfWork.SaveChangesAsync();
            return original.ToVm();
        }
    }
}