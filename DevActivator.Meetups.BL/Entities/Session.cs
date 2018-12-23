using System;

namespace DevActivator.Meetups.BL.Entities
{
    public class Session
    {
        public string TalkId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}