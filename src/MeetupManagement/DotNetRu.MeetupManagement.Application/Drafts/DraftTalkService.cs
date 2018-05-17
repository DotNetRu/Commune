namespace DotNetRu.MeetupManagement.Application.Drafts
{
    internal class DraftTalkService : IDraftTalkService
    {
        private readonly Core.Drafts.IDraftTalkService _draftTalkService;

        public DraftTalkService(Core.Drafts.IDraftTalkService draftTalkService)
        {
            _draftTalkService = draftTalkService;
        }

        //[Transactional]
        public long CreateDraftTalk(long communityId, string title, string speakerName, string speakerContacts)
        {
            var draftTalk = _draftTalkService.CreateDraftTalk(communityId, title, speakerName, speakerContacts);
            return draftTalk.Id;
        }
    }
}