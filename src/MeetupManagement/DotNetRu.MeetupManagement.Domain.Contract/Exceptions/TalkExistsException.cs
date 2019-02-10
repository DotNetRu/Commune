using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetRu.MeetupManagement.Domain.Contract.Exceptions
{
#pragma warning disable CA1032 // Implement standard exception constructors
    public sealed class TalkExistsException : EntityExistsException
#pragma warning restore CA1032 // Implement standard exception constructors
    {
        public TalkExistsException(string talkId, string message)
            : base(message)
        {
            TalkId = talkId;
        }

        public string TalkId { get; }
    }
}
