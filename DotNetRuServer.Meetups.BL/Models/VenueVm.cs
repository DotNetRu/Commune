using System.Xml.Serialization;
using DotNetRuServer.Comon.BL.Enums;

namespace DotNetRuServer.Meetups.BL.Models
{
    [XmlRoot(ElementName = "Venue")]
    public class VenueVm
    {
        public string Id { get; set; }

        [XmlIgnore]
        public City City { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string MapUrl { get; set; }
    }
}