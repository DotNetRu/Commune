using System.Collections.Generic;
using System.Xml.Serialization;

namespace DotNetRuServer.Meetups.BL.Models
{
    [XmlRoot(ElementName = "Meetup")]
    public class MeetupVm
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string CommunityId { get; set; }

        [XmlArrayItem("FriendId")]
        public List<string> FriendIds { get; set; }

        public string VenueId { get; set; }

        [XmlArray("Sessions")]
        [XmlArrayItem("Session")]
        public List<SessionVm> Sessions { get; set; }
    }
}