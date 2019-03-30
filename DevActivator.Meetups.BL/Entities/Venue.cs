using System.Collections.Generic;

namespace DevActivator.Meetups.BL.Entities
{
    public class Venue
    {
        public int Id { get; set; }
        public string ExportId { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }
        public string MapUrl { get; set; }


        public List<Meetup> Meetups { get; set; }
    }
}