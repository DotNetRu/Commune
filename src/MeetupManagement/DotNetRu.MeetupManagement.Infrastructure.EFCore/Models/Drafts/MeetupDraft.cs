namespace DotNetRu.MeetupManagement.Infrastructure.EFCore.Models.Drafts
{
    using System.Collections.Generic;
    using FriendReference = DotNetRu.MeetupManagement.Infrastructure.EFCore.Models.EntityReference;
    using VenueReference = DotNetRu.MeetupManagement.Infrastructure.EFCore.Models.EntityReference;

    public class MeetupDraft
    {
        public MeetupDraft()
        {
            Talks = new HashSet<TalkDraft>();
            Friends = new HashSet<FriendReference>();
        }

        public MeetupDraft(string communityId, string id)
            : this()
        {
            CommunityId = communityId;
            Id = id;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<TalkDraft> Talks { get; private set; }
        public virtual ICollection<FriendReference> Friends { get; private set; }
        public virtual VenueReference Venue { get; set; }

        public string VenueId { get; set; }
        public string CommunityId { get; set; }
        public string Id { get; set; }
    }
}