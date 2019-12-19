using System.Collections.Generic;
using System.Xml.Serialization;

namespace DotNetRuServer.Meetups.BL.Models
{
    [XmlRoot("Talk")]
    public class TalkExportVm
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

    public class TalkVm
    {
        public string Id { get; set; }

        public List<SpeakerReference> SpeakerIds { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string CodeUrl { get; set; }

        public string SlidesUrl { get; set; }

        public string VideoUrl { get; set; }
    }

    public class SpeakerReference
    {
        public string SpeakerId { get; set; }
    }
}
