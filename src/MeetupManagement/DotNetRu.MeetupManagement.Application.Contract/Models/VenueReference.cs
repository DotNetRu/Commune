using System;

namespace DotNetRu.MeetupManagement.Application.Contract.Models
{
    public class VenueReference
    {
        public VenueReference(string id, string name, bool isDraft)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            IsDraft = isDraft;
        }

        public string Id { get; }
        public string Name { get; }
        public bool IsDraft { get; }
    }
}
