using System.Collections.Generic;

namespace DotNetRu.MeetupManagement.Core.Drafts
{
    public class DraftTalk
    {
        public long Id { get; set; }
        public long CommunityId { get; set; }
        public string Title { get; set; }
        public string Comments { get; set; }
        public string SpeakerName { get; set; }
        public string SpeakerContacts { get; set; }
        //public long? SpeakerPersonId { get; set; }
        //public IReadOnlyCollection<TalkTryout> Tryouts { get; set; }
    }
}
