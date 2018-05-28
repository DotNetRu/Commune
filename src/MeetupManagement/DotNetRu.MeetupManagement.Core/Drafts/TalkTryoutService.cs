using DotNetRu.MeetupManagement.Core.Drafts.Events;
using DotNetRu.MeetupManagement.Core.Shared;

namespace DotNetRu.MeetupManagement.Core.Drafts
{
    internal class TalkTryoutService : ITalkTryoutService
    {
        private readonly ITalkTryoutRepository _talkTryoutRepository;
        private readonly IEventBus _eventBus;

        public TalkTryoutService(ITalkTryoutRepository talkTryoutRepository, IEventBus eventBus)
        {
            _talkTryoutRepository = talkTryoutRepository;
            _eventBus = eventBus;
        }

        public void CommitTryout(long talkId, string comment)  // MakeTryout? FinishTryout?
        {
            var tryout = new TalkTryout()
                         {
                             DraftTalkId = talkId,
                             Comment = comment
                         };
            _talkTryoutRepository.Create(tryout);

            _eventBus.Publish(new TalkTryoutCommited
                              {
                                  TryoutId = tryout.Id,
                                  Description = tryout.Comment
                              });
        }

        public void AttachArtifacts(long tryoutId, byte[] artifacts)
        {
            _talkTryoutRepository.SaveArtifacts(tryoutId, artifacts);
        }
    }
}
