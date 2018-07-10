using System;

namespace DotNetRu.MeetupManagement.Domain.Contract.Exceptions
{
    public class MeetupNotFoundException : EntityNotFoundException
    {
        public MeetupNotFoundException(string meetupId, string message) : base(message)
        {
            MeetupId = meetupId ?? throw new ArgumentNullException(nameof(meetupId));
        }

        public string MeetupId { get; }
    }
}
