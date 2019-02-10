using System;

namespace DotNetRu.MeetupManagement.Application.Contract.Models
{
    public class TalkRehearsal
    {
        public TalkRehearsal(string id, DateTimeOffset time)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Time = time;
        }

        public string Id { get; }
        public DateTimeOffset Time { get; set; }
        public string Comment { get; set; }
        public string SlidesUrl { get; set; }
        public string VideoUrl { get; set; }
    }
}
