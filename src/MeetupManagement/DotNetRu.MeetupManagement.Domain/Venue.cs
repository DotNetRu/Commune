using System;

namespace DotNetRu.MeetupManagement.Domain
{
    public class Venue
    {
        public Venue(string id, string name)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public string Id { get; }
        public string Name { get; }
        public string Url { get; set; }
        public string Description { get; set; }
    }
}
