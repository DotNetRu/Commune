using System;
using System.Collections.Generic;

namespace DotNetRu.MeetupManagement.Application.Contract.Models
{
    public class TalkDraft
    {
        public TalkDraft(string id)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Speakers = new List<SpeakerReference>();
            Rehearsals = new List<TalkRehearsal>();
        }

        public string Id { get; set; }
        public string CommunityId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public MeetupReference MeetupDraft { get; set; }
        public ICollection<SpeakerReference> Speakers { get; }
        public ICollection<TalkRehearsal> Rehearsals { get; }
    }
}
