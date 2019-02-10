using System;

namespace DotNetRu.MeetupManagement.Application.Contract.Models
{
    public class MeetupReference
    {
        public MeetupReference(string communityId, string id, string name)
        {
            CommunityId = communityId ?? throw new ArgumentNullException(nameof(communityId));
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public string CommunityId { get; }
        public string Id { get; }
        public string Name { get; }
    }
}
