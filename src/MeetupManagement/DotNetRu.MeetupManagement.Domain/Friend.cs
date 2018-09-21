using System;

namespace DotNetRu.MeetupManagement.Domain
{
#pragma warning disable CA1716 // Identifiers should not match keywords
    public class Friend
#pragma warning restore CA1716 // Identifiers should not match keywords
    {
        public Friend(string id, string name)
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
