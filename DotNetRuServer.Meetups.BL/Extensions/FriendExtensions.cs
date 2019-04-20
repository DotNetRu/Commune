using System;
using DotNetRuServer.Meetups.BL.Entities;
using DotNetRuServer.Meetups.BL.Models;

namespace DotNetRuServer.Meetups.BL.Extensions
{
    public static class FriendExtensions
    {
        public static FriendVm EnsureIsValid(this FriendVm friend)
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

        public static Friend Extend(this Friend original, FriendVm friend)
            => new Friend
            {
                Id = original.Id,
                ExportId = friend.Id,
                Name = friend.Name,
                Url = friend.Url,
                Description = friend.Description
            };

        public static FriendVm ToVm(this Friend original) => new FriendVm
        {
            Id = original.ExportId,
            Name = original.Name,
            Url = original.Url,
            Description = original.Description
        };

    }
}