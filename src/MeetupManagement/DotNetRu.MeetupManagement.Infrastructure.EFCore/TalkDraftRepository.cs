using DotNetRu.MeetupManagement.Domain;
using DotNetRu.MeetupManagement.Domain.Drafts;

namespace DotNetRu.MeetupManagement.Infrastructure.EFCore
{
    public class TalkDraftRepository : ITalkDraftRepository
    {
        /// <exception cref="Domain.Contract.Exceptions.CommunityNotFoundException" />
        public TalkDraft Create(string communityId, string title, string comments)
        {
            throw new System.NotImplementedException();
        }

        /// <exception cref="Domain.Contract.Exceptions.CommunityNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.TalkNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.SpeakerNotFoundException" />
        public EntityReference AddSpeaker(TalkKey key, string speakerId)
        {
            throw new System.NotImplementedException();
        }

        /// <exception cref="Domain.Contract.Exceptions.CommunityNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.TalkNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.SpeakerNotFoundException" />
        public void RemoveSpeaker(TalkKey key, string speakerId)
        {
            throw new System.NotImplementedException();
        }

        /// <exception cref="Domain.Contract.Exceptions.CommunityNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.TalkNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.SpeakerNotFoundException" />
        public void Update(TalkDraft entity)
        {
            throw new System.NotImplementedException();
        }

        /// <exception cref="Domain.Contract.Exceptions.CommunityNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.TalkNotFoundException" />
        public void Delete(TalkKey id)
        {
            throw new System.NotImplementedException();
        }

        /// <exception cref="Domain.Contract.Exceptions.CommunityNotFoundException" />
        /// <exception cref="Domain.Contract.Exceptions.TalkNotFoundException" />
        public TalkDraft Get(TalkKey id)
        {
            throw new System.NotImplementedException();
        }
    }
}