using System.Xml.Serialization;

namespace DotNetRuServer.Meetups.BL.Models
{
    [XmlRoot(ElementName = "Friend")]
    public class FriendVm
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }

        public string LogoId { get; set; }
        public string SmallLogoId { get; set; }
    }
}