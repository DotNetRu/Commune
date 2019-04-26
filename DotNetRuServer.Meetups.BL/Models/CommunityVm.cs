using System;
using System.Xml.Serialization;

namespace DotNetRuServer.Meetups.BL.Models
{
    [XmlRoot(ElementName = "Community")]
    public class CommunityVm
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public string City { get; set; }
        public string TimeZone { get; set; }
    }
}