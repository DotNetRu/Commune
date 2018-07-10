using System;

namespace DotNetRu.MeetupManagement.Domain.Contract.Exceptions
{
    public class TalkNotFoundException: EntityNotFoundException
    {
        public TalkNotFoundException(string talkId, string message) : base(message)
        {
            TalkId = talkId ?? throw new ArgumentNullException(nameof(talkId));
        }

        public string TalkId { get; }
    }
}
