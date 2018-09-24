using System;

namespace DotNetRu.MeetupManagement.Domain
{
    public class VenueKey
    {
        public VenueKey(string communityId, string id)
        {
            CommunityId = communityId ?? throw new ArgumentNullException(nameof(communityId));
            Id = id ?? throw new ArgumentNullException(nameof(id));
        }

        public string CommunityId { get; }
        public string Id { get; }
    }
}
