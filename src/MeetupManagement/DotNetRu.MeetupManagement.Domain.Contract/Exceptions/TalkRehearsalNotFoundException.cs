using System;

namespace DotNetRu.MeetupManagement.Domain.Contract.Exceptions
{
#pragma warning disable CA1032 // Implement standard exception constructors
    public class TalkRehearsalNotFoundException : EntityNotFoundException
#pragma warning restore CA1032 // Implement standard exception constructors
    {
        public TalkRehearsalNotFoundException(string rehearsalId, string message)
            : base(message)
        {
            RehearsalId = rehearsalId;
        }

        public TalkRehearsalNotFoundException(string rehearsalId, string message, Exception innerException)
            : base(message, innerException)
        {
            RehearsalId = rehearsalId;
        }

        public string RehearsalId { get; }
    }
}
