using System.Collections.Generic;
using System.Xml.Serialization;

namespace DotNetRuServer.Meetups.BL.Models
{
    [XmlRoot(elementName:"Talk")]
    public class TalkVm
    {
        public string Id { get; set; }

        [XmlArrayItem(ElementName = "SpeakerId")]
        public List<string> SpeakerIds { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string CodeUrl { get; set; }

        public string SlidesUrl { get; set; }

        public string VideoUrl { get; set; }
    }
}