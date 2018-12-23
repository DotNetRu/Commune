using System;
using DevActivator.Meetups.BL.Entities;

namespace DevActivator.Meetups.BL.Extensions
{
    public static class FriendExtensions
    {
        public static Friend EnsureIsValid(this Friend friend)
        {
            // todo: implement full validation
            if (string.IsNullOrWhiteSpace(friend.Name))
            {
                throw new FormatException(nameof(friend.Name));
            }

            if (string.IsNullOrWhiteSpace(friend.Url))
            {
                throw new FormatException(nameof(friend.Url));
            }

            if (string.IsNullOrWhiteSpace(friend.Description))
            {
                throw new FormatException(nameof(friend.Description));
            }

            return friend;
        }

        public static Friend Extend(this Friend original, Friend friend)
            => new Friend
            {
                Id = original.Id,
                Name = friend.Name,
                Url = friend.Url,
                Description = friend.Description
            };
    }
}