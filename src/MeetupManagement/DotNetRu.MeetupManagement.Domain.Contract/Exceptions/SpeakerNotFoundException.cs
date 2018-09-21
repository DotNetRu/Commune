using System;

namespace DotNetRu.MeetupManagement.Domain.Contract.Exceptions
{
#pragma warning disable CA1032 // Implement standard exception constructors
    public class SpeakerNotFoundException : EntityNotFoundException
#pragma warning restore CA1032 // Implement standard exception constructors
    {
        public SpeakerNotFoundException(string speakerId, string message)
            : base(message)
        {
            SpeakerId = speakerId ?? throw new ArgumentNullException(nameof(speakerId));
        }

        public SpeakerNotFoundException(string speakerId, string message, Exception innerException)
            : base(message, innerException)
        {
            SpeakerId = speakerId ?? throw new ArgumentNullException(nameof(speakerId));
        }

        public string SpeakerId { get; }
    }
}
