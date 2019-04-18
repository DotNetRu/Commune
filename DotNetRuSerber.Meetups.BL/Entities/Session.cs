using System;

namespace DotNetRuSerber.Meetups.BL.Entities
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