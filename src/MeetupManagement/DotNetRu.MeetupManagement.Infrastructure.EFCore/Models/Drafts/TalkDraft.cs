namespace DotNetRu.MeetupManagement.Infrastructure.EFCore.Models.Drafts
{
    using System.Collections.Generic;
    using SpeakerReference = DotNetRu.MeetupManagement.Infrastructure.EFCore.Models.EntityReference;

    public class TalkDraft
    {
        public TalkDraft()
        {
            Speakers = new HashSet<SpeakerReference>();
            Rehearsals = new HashSet<TalkRehearsal>();
        }

        public TalkDraft(string communityId, string id)
            : this()
        {
            CommunityId = communityId;
            Id = id;
        }

        public string Title { get; set; }
        public string Comments { get; set; }
        public virtual ICollection<SpeakerReference> Speakers { get; private set; }
        public virtual ICollection<TalkRehearsal> Rehearsals { get; private set; }

        public string CommunityId { get; set; }
        public string Id { get; set; }
    }
}
