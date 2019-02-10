using System;
using SpeakerReference = DotNetRu.MeetupManagement.Domain.EntityReference;

namespace DotNetRu.MeetupManagement.Domain.Drafts
{
    public class TalkDraftService : ITalkDraftService
    {
        private readonly ITalkDraftRepository _talkDraftRepository;
        private readonly ITalkRehearsalRepository _talkRehearsalRepository;

        public TalkDraftService(
            ITalkDraftRepository talkDraftRepository,
            ITalkRehearsalRepository talkRehearsalRepository)
        {
            _talkDraftRepository = talkDraftRepository ?? throw new ArgumentNullException(nameof(talkDraftRepository));
            _talkRehearsalRepository = talkRehearsalRepository ?? throw new ArgumentNullException(nameof(talkRehearsalRepository));
        }

        public TalkDraft GetTalkDraft(TalkKey key)
        {
            return _talkDraftRepository.GetEntity(key);
        }

        public TalkDraft CreateTalkDraft(string communityId, string id, string title, string comments)
        {
            return _talkDraftRepository.Create(communityId,  id, title, comments);
        }

        public void DeleteTalkDraft(TalkKey key)
        {
            _talkDraftRepository.Delete(key);
        }

        public SpeakerReference AddSpeaker(TalkKey key, string speakerDraftId)
        {
            return _talkDraftRepository.AddSpeaker(key, speakerDraftId);
        }

        public void RemoveSpeaker(TalkKey key, string speakerId)
        {
            _talkDraftRepository.RemoveSpeaker(key, speakerId);
        }

        public TalkRehearsal AddRehearsal(TalkKey key, string comments, DateTimeOffset time)
        {
            return _talkRehearsalRepository.Add(key, comments, time);
        }

        public void UpdateRehearsal(TalkKey key, TalkRehearsal rehearsal)
        {
            _talkRehearsalRepository.Update(rehearsal);
        }
    }
}