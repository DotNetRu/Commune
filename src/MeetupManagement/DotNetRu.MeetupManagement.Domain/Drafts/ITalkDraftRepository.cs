using DotNetRu.MeetupManagement.Domain.Common;

namespace DotNetRu.MeetupManagement.Domain.Drafts
{
    public interface ITalkDraftRepository : IRepository<TalkDraft, TalkKey>
    {
        /// <summary>
        /// Store talk draft
        /// </summary>
        /// <param name="communityId"></param>
        /// <param name="title"></param>
        /// <param name="comments"></param>
        /// <exception cref="Contract.Exceptions.CommunityNotFoundException" />
        /// <exception cref="Contract.Exceptions.TalkNotFoundException" />
        TalkDraft Create(string communityId, string title, string comments);

        /// <summary>
        /// Store association speaker reference for talk draft
        /// </summary>
        /// <exception cref="Contract.Exceptions.CommunityNotFoundException" />
        /// <exception cref="Contract.Exceptions.TalkNotFoundException" />
        /// <exception cref="Contract.Exceptions.SpeakerNotFoundException" />
        EntityReference AddSpeaker(TalkKey key, string speakerId);

        /// <summary>
        /// Remove association speaker reference from talk draft
        /// </summary>
        /// <exception cref="Contract.Exceptions.CommunityNotFoundException" />
        /// <exception cref="Contract.Exceptions.TalkNotFoundException" />
        /// <exception cref="Contract.Exceptions.SpeakerNotFoundException" />
        void RemoveSpeaker(TalkKey key, string speakerId);
    }
}