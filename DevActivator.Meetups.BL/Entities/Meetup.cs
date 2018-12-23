using System.Collections.Generic;
using System.Xml.Serialization;
using DevActivator.Common.BL.Config;

namespace DevActivator.Meetups.BL.Entities
{
    public class Meetup : IFlatEntity
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string CommunityId { get; set; }

        /// <summary> Obsolete </summary>
        public string Date { get; set; }

        [XmlArrayItem("FriendId")]
        public List<string> FriendIds { get; set; }

        public string VenueId { get; set; }

        public List<Session> Sessions { get; set; }

        [XmlArrayItem("TalkId")]
        public List<string> TalkIds { get; set; }
    }
}