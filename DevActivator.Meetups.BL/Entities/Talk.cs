using System.Collections.Generic;

namespace DevActivator.Meetups.BL.Entities
{
    public class Talk
    {
        public int Id { get; set; }
        public string ExportId { get; set; }

        public List<SpeakerTalk> Speakers { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public List<SeeAlsoTalk> SeeAlsoTalks { get; set; }

        public string CodeUrl { get; set; }
        public string SlidesUrl { get; set; }
        public string VideoUrl { get; set; }
    }
}