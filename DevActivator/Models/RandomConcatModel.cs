using System.Collections.Generic;
using DevActivator.Meetups.BL.Models;

namespace DevActivator.Models
{
    public class RandomConcatModel
    {
        public string Name { get; set; }

        public string CommunityId { get; set; }

        public string VenueId { get; set; }

        public List<SessionVm> Sessions { get; set; }

        public List<string> TalkIds { get; set; }

        public List<string> SpeakerIds { get; set; }

        public List<string> FriendIds { get; set; }
    }
}