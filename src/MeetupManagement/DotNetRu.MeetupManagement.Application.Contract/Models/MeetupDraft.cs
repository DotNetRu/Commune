using System;
using System.Collections.Generic;

namespace DotNetRu.MeetupManagement.Application.Contract.Models
{
    public class MeetupDraft
    {
        public MeetupDraft(string id, string communityId)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            CommunityId = communityId ?? throw new ArgumentNullException(nameof(communityId));
            Talks = new List<TalkDraft>();
            Friends = new List<FriendReference>();
        }

        public string Id { get; }
        public string CommunityId { get; }
        public ICollection<TalkDraft> Talks { get; }
        public ICollection<FriendReference> Friends { get; }
        public VenueReference Venue { get; set; }
    }
}
