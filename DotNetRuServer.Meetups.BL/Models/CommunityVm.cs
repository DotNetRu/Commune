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

        [XmlElement(ElementName = "VkUrl")]
        public string Vk { get; set; }

        [XmlElement(ElementName = "TelegramChannelUrl")]
        public string TelegramChannel { get; set; }

        [XmlElement(ElementName = "TelegramChatUrl")]
        public string TelegramChat { get; set; }

        [XmlElement(ElementName = "TimePadUrl")]
        public string TimePad { get; set; }
    }
}
