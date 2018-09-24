using System;

namespace DotNetRu.MeetupManagement.Domain.Contract.Exceptions
{
#pragma warning disable CA1032 // Implement standard exception constructors
    public class TalkNotFoundException : EntityNotFoundException
#pragma warning restore CA1032 // Implement standard exception constructors
    {
        public TalkNotFoundException(string talkId, string message)
            : base(message)
        {
            TalkId = talkId ?? throw new ArgumentNullException(nameof(talkId));
        }

        public TalkNotFoundException(string talkId, string message, Exception innerException)
            : base(message, innerException)
        {
            TalkId = talkId ?? throw new ArgumentNullException(nameof(talkId));
        }

        public string TalkId { get; }
    }
}
