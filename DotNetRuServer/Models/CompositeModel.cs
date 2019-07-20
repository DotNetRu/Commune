using System.Collections.Generic;
using DotNetRuServer.Meetups.BL.Models;

namespace DotNetRuServer.Models
{
    public class CompositeModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public Communities? CommunityId { get; set; }

        public VenueVm Venue { get; set; }

        public List<SessionVm> Sessions { get; set; }

        public Dictionary<string, TalkVm> Talks { get; set; }

        public Dictionary<string, SpeakerVm> Speakers { get; set; }

        public List<FriendVm> Friends { get; set; }
    }
}