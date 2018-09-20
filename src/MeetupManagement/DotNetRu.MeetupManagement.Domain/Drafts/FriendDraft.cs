using System;

namespace DotNetRu.MeetupManagement.Domain.Drafts
{
    public class FriendDraft
    {
        public FriendDraft(string id)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
        }

        public string Id { get; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
    }
}
