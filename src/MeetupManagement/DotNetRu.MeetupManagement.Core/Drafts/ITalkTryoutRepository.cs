using DotNetRu.MeetupManagement.Core.Shared;

namespace DotNetRu.MeetupManagement.Core.Drafts
{
    public interface ITalkTryoutRepository : IRepository<TalkTryout>
    {
        void SaveArtifacts(long tryoutId, byte[] artifacts);
    }
}