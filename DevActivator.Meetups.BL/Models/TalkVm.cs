using System.Collections.Generic;
using DevActivator.Meetups.BL.Entities;

namespace DevActivator.Meetups.BL.Models
{
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
}