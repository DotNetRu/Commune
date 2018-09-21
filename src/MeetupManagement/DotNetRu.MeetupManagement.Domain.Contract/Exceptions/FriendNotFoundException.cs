using System;

namespace DotNetRu.MeetupManagement.Domain.Contract.Exceptions
{
#pragma warning disable CA1032 // Implement standard exception constructors
    public class FriendNotFoundException : EntityNotFoundException
#pragma warning restore CA1032 // Implement standard exception constructors
    {
        public FriendNotFoundException(string friendId, string message)
            : base(message)
        {
            FriendId = friendId ?? throw new ArgumentNullException(nameof(friendId));
        }

        public FriendNotFoundException(string friendId, string message, Exception innerException)
            : base(message, innerException)
        {
            FriendId = friendId ?? throw new ArgumentNullException(nameof(friendId));
        }

        public string FriendId { get; }
    }
}
