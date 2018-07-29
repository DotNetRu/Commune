using System;

namespace DotNetRu.MeetupManagement.Domain.Contract.Exceptions
{
    public class FriendNotFoundException : EntityNotFoundException
    {
        public FriendNotFoundException(string friendId, string message) : base(message)
        {
            FriendId = friendId ?? throw new ArgumentNullException(nameof(friendId));
        }

        public string FriendId { get; }
    }
}
