using DotNetRu.MeetupManagement.Domain.Common;

namespace DotNetRu.MeetupManagement.Domain.Drafts
{
    public interface IFriendDraftRepository : IRepository<FriendDraft, string>
    {
        FriendDraft Add(CreateFriendDraftParameters parameters);
    }
}
