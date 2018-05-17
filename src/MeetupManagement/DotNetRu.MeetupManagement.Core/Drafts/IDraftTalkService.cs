namespace DotNetRu.MeetupManagement.Core.Drafts
{
    public interface IDraftTalkService
    {
        DraftTalk CreateDraftTalk(long communityId, string title, string speakerName, string speakerContacts);
    }
}