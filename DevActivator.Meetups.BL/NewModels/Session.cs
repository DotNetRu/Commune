using System;

namespace DevActivator.Meetups.BL.NewModels
{
    public class Session
    {
        public int Id { get; set; }

        public int TalkId { get; set; }
        public Talk Talk { get; set; }

        public int MeetupId { get; set; }
        public Meetup Meetup { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}