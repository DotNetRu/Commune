using System.Collections.Generic;
using System.Xml.Serialization;

namespace DotNetRuServer.Meetups.BL.Models
{
    [XmlRoot(ElementName = "Meetup")]
    public class MeetupVm
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public Communities CommunityId { get; set; }

        [XmlArrayItem("FriendId")] public List<FriendReference> FriendIds { get; set; }

        public string VenueId { get; set; }

        [XmlArray("Sessions")]
        [XmlArrayItem("Session")]
        public List<SessionVm> Sessions { get; set; }
    }

    public class FriendReference
    {
        public string FriendId { get; set; }
    }

    public enum Communities
    {
        SpbDotNet = 1,
        MskDotNet = 2,
        SarDotNet = 3,
        KryDotNet = 4,
        KznDotNet = 5,
        NskDotNet = 6,
        NnvDotNet = 7,
        UfaDotNet = 8
    }
}