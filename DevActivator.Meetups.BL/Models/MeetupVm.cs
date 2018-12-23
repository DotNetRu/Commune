using System.Collections.Generic;
using DevActivator.Meetups.BL.Entities;
using DevActivator.Meetups.BL.Enums;

namespace DevActivator.Meetups.BL.Models
{
    public class MeetupVm
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public Community CommunityId { get; set; }

        public List<FriendReference> FriendIds { get; set; }

        public string VenueId { get; set; }

        public List<Session> Sessions { get; set; }
    }
}