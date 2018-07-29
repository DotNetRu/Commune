using System;
using System.Collections.Generic;
using SpeakerReference = DotNetRu.MeetupManagement.Domain.EntityReference;

namespace DotNetRu.MeetupManagement.Domain.Drafts
{
    public class TalkDraft
    {
        public TalkDraft(TalkKey key)
        {
            Key = key ?? throw new ArgumentNullException(nameof(key));
            Speakers = new List<SpeakerReference>();
            Rehearsals = new List<TalkRehearsal>();
        } 
        public TalkKey Key { get; }
        public string Title { get; set; }
        public string Comments { get; set; }

        public ICollection<SpeakerReference> Speakers { get; }
        public ICollection<TalkRehearsal> Rehearsals { get; set; }
    }
}
