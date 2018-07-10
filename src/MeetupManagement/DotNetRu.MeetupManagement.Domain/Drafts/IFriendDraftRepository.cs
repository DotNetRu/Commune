using DotNetRu.MeetupManagement.Domain.Shared;

namespace DotNetRu.MeetupManagement.Domain.Drafts
{
    public interface IFriendDraftRepository : IRepository<SpeakerDraft, string>
    {
        FriendDraft Add(CreateFriendDraftParameters parameters);
    }
}
