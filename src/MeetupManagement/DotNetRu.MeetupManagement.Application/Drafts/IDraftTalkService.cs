namespace DotNetRu.MeetupManagement.Application.Drafts
{
    public interface IDraftTalkService : IApplicationService
    {
        long CreateDraftTalk(long communityId, string title, string speakerName, string speakerContacts);
    }

    public interface IApplicationService { }

}