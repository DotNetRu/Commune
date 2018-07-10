using System;

namespace DotNetRu.MeetupManagement.Domain.Drafts
{
    public class TalkRehearsalKey
    {
        public TalkRehearsalKey(TalkKey talkDraftKey, string id)
        {
            TalkKey = talkDraftKey ?? throw new ArgumentNullException(nameof(talkDraftKey));
            Id = id ?? throw new ArgumentNullException(nameof(id));
        }

        public TalkKey TalkKey { get; }
        public string Id { get; }

    }
}
