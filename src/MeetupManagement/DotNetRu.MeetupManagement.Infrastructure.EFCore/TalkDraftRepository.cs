using DotNetRu.MeetupManagement.Domain;
using DotNetRu.MeetupManagement.Domain.Drafts;

namespace DotNetRu.MeetupManagement.Infrastructure.EFCore
{
    public class TalkDraftRepository : ITalkDraftRepository
    {
        public TalkDraft Create(string communityId, string title, string comments)
        {
            throw new System.NotImplementedException();
        }

        public EntityReference AddSpeaker(TalkKey key, string speakerId)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveSpeaker(TalkKey key, string speakerId)
        {
            throw new System.NotImplementedException();
        }

        public void Update(TalkDraft entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(TalkKey id)
        {
            throw new System.NotImplementedException();
        }

        public TalkDraft Get(TalkKey id)
        {
            throw new System.NotImplementedException();
        }
    }
}