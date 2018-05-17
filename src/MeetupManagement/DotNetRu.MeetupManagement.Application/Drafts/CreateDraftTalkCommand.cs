namespace DotNetRu.MeetupManagement.Application.Drafts
{
    public class CreateDraftTalkCommand : ICommand
    {
        private readonly Core.Drafts.IDraftTalkService _draftTalkService;

        public CreateDraftTalkCommand(Core.Drafts.IDraftTalkService draftTalkService)
        {
            _draftTalkService = draftTalkService;
        }

        //[Transactional]
        public long Execute(long communityId, string title, string speakerName, string speakerContacts)
        {
            var draftTalk = _draftTalkService.CreateDraftTalk(communityId, title, speakerName, speakerContacts);
            return draftTalk.Id;
        }
    }

    public interface ICommand { }
}
