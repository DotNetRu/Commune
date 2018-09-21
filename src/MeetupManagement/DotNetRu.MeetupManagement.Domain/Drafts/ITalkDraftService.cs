using System;
using SpeakerReference = DotNetRu.MeetupManagement.Domain.EntityReference;

namespace DotNetRu.MeetupManagement.Domain.Drafts
{
    public interface ITalkDraftService
    {
        /// <exception cref="Contract.Exceptions.CommunityNotFoundException" />
        /// <exception cref="Contract.Exceptions.TalkNotFoundException" />
        TalkDraft GetTalkDraft(TalkKey key);

        /// <exception cref="Contract.Exceptions.CommunityNotFoundException" />
        TalkDraft CreateTalkDraft(string communityId, string title, string comments);

        /// <exception cref="Contract.Exceptions.CommunityNotFoundException" />
        /// <exception cref="Contract.Exceptions.TalkNotFoundException" />
        void DeleteTalkDraft(TalkKey talkDraftKey);

        /// <exception cref="Contract.Exceptions.CommunityNotFoundException" />
        /// <exception cref="Contract.Exceptions.TalkNotFoundException" />
        /// <exception cref="Contract.Exceptions.SpeakerNotFoundException" />
        SpeakerReference AddSpeaker(TalkKey key, string speakerId);

        /// <exception cref="Contract.Exceptions.CommunityNotFoundException" />
        /// <exception cref="Contract.Exceptions.TalkNotFoundException" />
        /// <exception cref="Contract.Exceptions.SpeakerNotFoundException" />
        void RemoveSpeaker(TalkKey key, string speakerId);

        /// <exception cref="Contract.Exceptions.CommunityNotFoundException" />
        /// <exception cref="Contract.Exceptions.TalkNotFoundException" />
        TalkRehearsal AddRehearsal(TalkKey key, string comments, DateTimeOffset time);

        /// <exception cref="Domain.Contract.Exceptions.CommunityNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.TalkNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.TalkRehearsalNotFoundException" />
        void UpdateRehearsal(TalkKey key, TalkRehearsal rehearsal);
    }
}