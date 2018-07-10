using System;

namespace DotNetRu.MeetupManagement.Domain
{
    public class MeetupKey
    {
        public MeetupKey(string communityId, string id)
        {
            CommunityId = communityId ?? throw new ArgumentNullException(nameof(communityId));
            Id = id ?? throw new ArgumentNullException(nameof(id));

        }

        public string Id { get; }
        public string CommunityId { get; }
    }
}
