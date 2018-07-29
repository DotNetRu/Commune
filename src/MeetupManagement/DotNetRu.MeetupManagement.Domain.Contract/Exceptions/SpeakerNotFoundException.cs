using System;

namespace DotNetRu.MeetupManagement.Domain.Contract.Exceptions
{
    public class SpeakerNotFoundException : EntityNotFoundException
    {
        public SpeakerNotFoundException(string speakerId, string message) : base(message)
        {
            SpeakerId = speakerId ?? throw new ArgumentNullException(nameof(speakerId));
        }

        public string SpeakerId { get; }
    }
}
