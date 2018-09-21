using System;

namespace DotNetRu.MeetupManagement.Domain.Contract.Exceptions
{
#pragma warning disable CA1032 // Implement standard exception constructors
    public class MeetupNotFoundException : EntityNotFoundException
#pragma warning restore CA1032 // Implement standard exception constructors
    {
        public MeetupNotFoundException(string meetupId, string message)
            : base(message)
        {
            MeetupId = meetupId ?? throw new ArgumentNullException(nameof(meetupId));
        }

        public MeetupNotFoundException(string meetupId, string message, Exception innerException)
            : base(message, innerException)
        {
            MeetupId = meetupId ?? throw new ArgumentNullException(nameof(meetupId));
        }

        public string MeetupId { get; }
    }
}
