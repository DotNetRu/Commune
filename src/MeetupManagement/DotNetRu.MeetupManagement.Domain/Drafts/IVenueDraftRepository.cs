using DotNetRu.MeetupManagement.Domain.Common;

namespace DotNetRu.MeetupManagement.Domain.Drafts
{
    public interface IVenueDraftRepository : IRepository<VenueDraft, string>
    {
        VenueDraft Add(CreateVenueDraftParameters parameters);
    }
}
