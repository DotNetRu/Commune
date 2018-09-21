using DotNetRu.MeetupManagement.Domain.Common;

namespace DotNetRu.MeetupManagement.Domain.Drafts
{
    public interface ISpeakerDraftRepository : IRepository<SpeakerDraft, string>
    {
        SpeakerDraft Add(CreateSpeakerDraftParameters parameters);
    }
}
