using System;

namespace DotNetRu.MeetupManagement.Application.Contract.Models
{
    public class SpeakerReference
    {
        public SpeakerReference(string id, string firstName, string lastName, bool isDraft)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            IsDraft = isDraft;
        }

        public string Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public bool IsDraft { get; }
    }
}
