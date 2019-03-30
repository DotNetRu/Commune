namespace DevActivator.Meetups.BL.Entities
{
    public class FriendAtMeetup
    {
        public int MeetupId { get; set; }
        public Meetup Meetup { get; set; }

        public int FriendId { get; set; }
        public Friend Friend { get; set; }
    }
}