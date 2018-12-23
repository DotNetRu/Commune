using System.Collections.Generic;
using System.Xml.Serialization;
using DevActivator.Common.BL.Config;

namespace DevActivator.Meetups.BL.Entities
{
    public class Talk : IFlatEntity
    {
        public string Id { get; set; }

        [XmlArrayItem("SpeakerId")]
        public List<string> SpeakerIds { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string CodeUrl { get; set; }

        public string SlidesUrl { get; set; }

        public string VideoUrl { get; set; }
    }
}