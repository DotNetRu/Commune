using DotNetRu.MeetupManagement.Core.Drafts;

namespace DotNetRu.MeetupManagement.Infrastructure.EFCore
{
    internal class TalkTryoutRepository : EfCoreRepository<TalkTryout>, ITalkTryoutRepository
    {
        public void SaveArtifacts(long tryoutId, byte[] artifacts)
        {
            // code to save artifacts
            throw new System.NotImplementedException();
        }
    }
}