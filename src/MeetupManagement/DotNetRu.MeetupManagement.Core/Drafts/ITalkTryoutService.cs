namespace DotNetRu.MeetupManagement.Core.Drafts
{
    internal interface ITalkTryoutService
    {
        void CommitTryout(long talkId, string comment);
        void AttachArtifacts(long tryoutId, byte[] artifacts);
    }
}