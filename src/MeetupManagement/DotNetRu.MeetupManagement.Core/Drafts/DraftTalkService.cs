using DotNetRu.MeetupManagement.Core.Drafts.Events;
using DotNetRu.MeetupManagement.Core.Shared;

namespace DotNetRu.MeetupManagement.Core.Drafts
{
    internal class DraftTalkService : IDraftTalkService
    {
        private readonly IDraftTalkRepository _draftTalkRepository;
        private readonly IEventBus _eventBus;

        public DraftTalkService(IDraftTalkRepository draftTalkRepository, IEventBus eventBus)
        {
            _draftTalkRepository = draftTalkRepository;
            _eventBus = eventBus;
        }

        public DraftTalk CreateDraftTalk(long communityId, string title, string speakerName, string speakerContacts)
        {
            var draftTalk = new DraftTalk()
                            {
                                Title = title,
                                SpeakerName = speakerName,
                                SpeakerContacts = speakerContacts
                            };

            _draftTalkRepository.Create(draftTalk);

            _eventBus.Publish(new DraftTalkCreated
                              {
                                  DraftTalkId = draftTalk.Id,
                                  Description = $"{speakerName} - {draftTalk.Title}"
                              });

            return draftTalk;
        }
    }
}