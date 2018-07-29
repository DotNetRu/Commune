using DotNetRu.MeetupManagement.Domain.Shared;

namespace DotNetRu.MeetupManagement.Domain.Drafts
{
    public interface IVenueDraftRepository : IRepository<VenueDraft, string>
    {
        VenueDraft Add(CreateVenueDraftParameters parameters);
    }
}
