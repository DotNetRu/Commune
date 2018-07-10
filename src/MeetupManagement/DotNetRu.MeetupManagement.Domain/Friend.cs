using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetRu.MeetupManagement.Domain
{
    public class Friend
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
