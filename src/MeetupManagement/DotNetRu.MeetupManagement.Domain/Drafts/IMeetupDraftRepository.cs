
using DotNetRu.MeetupManagement.Domain.Shared;

namespace DotNetRu.MeetupManagement.Domain.Drafts
{
    public interface IMeetupDraftRepository : IRepository<MeetupDraft, MeetupKey>
    {
        MeetupDraft Add(string name);
    }
}
