using System;
using System.Collections.Generic;
using FriendReference = DotNetRu.MeetupManagement.Domain.EntityReference;
using VenueReference = DotNetRu.MeetupManagement.Domain.EntityReference;

namespace DotNetRu.MeetupManagement.Domain.Drafts
{
    public class MeetupDraft
    {
        public MeetupDraft(MeetupKey key)
        {
            Key = key ?? throw new ArgumentNullException(nameof(key));
            Talks = new List<TalkDraft>();
            Friends = new List<FriendReference>();
        }

        public MeetupKey Key { get; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<TalkDraft> Talks { get; }
        public ICollection<FriendReference> Friends { get; }
        public VenueReference Venue { get; set; }
    }
}
