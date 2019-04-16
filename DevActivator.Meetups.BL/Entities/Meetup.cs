using System.Collections.Generic;

namespace DevActivator.Meetups.BL.Entities
{
    public class Meetup
    {
        public int Id { get; set; }
        public string ExportId { get; set; }

        public string Name { get; set; }

        public int CommunityId { get; set; }
        public Community Community { get; set; }

        public List<FriendAtMeetup> Friends { get; set; }

        public int VenueId { get; set; }
        public Venue Venue { get; set; }

        public List<Session> Sessions { get; set; }
    }
}