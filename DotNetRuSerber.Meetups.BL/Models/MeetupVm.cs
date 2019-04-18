using System.Collections.Generic;

namespace DotNetRuSerber.Meetups.BL.Models
{
    public class MeetupVm
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string CommunityId { get; set; }

        public List<string> FriendIds { get; set; }

        public string VenueId { get; set; }

        public List<SessionVm> Sessions { get; set; }
    }
}