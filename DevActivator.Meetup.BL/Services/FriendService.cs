using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevActivator.Meetup.BL.Entities;
using DevActivator.Meetup.BL.Extensions;
using DevActivator.Meetup.BL.Interfaces;
using DevActivator.Meetup.BL.Models;

namespace DevActivator.Meetup.BL.Services
{
    public class FriendService : IFriendService
    {
        private readonly IFriendProvider _friendProvider;

        public FriendService(IFriendProvider friendProvider)
        {
            _friendProvider = friendProvider;
        }

        public async Task<List<AutocompleteRow>> GetAllFriendsAsync()
        {
            var friends = await _friendProvider.GetAllFriendsAsync().ConfigureAwait(false);
            return friends
                .Select(x => new AutocompleteRow {Id = x.Id, Name = x.Name})
                .ToList();
        }

        public async Task<Friend> GetFriendAsync(string friendId)
        {
            var friend = await _friendProvider.GetFriendOrDefaultAsync(friendId).ConfigureAwait(false);
            return friend;
        }

        public async Task<Friend> AddFriendAsync(Friend friend)
        {
            friend.EnsureIsValid();

            var original = await _friendProvider.GetFriendOrDefaultAsync(friend.Id).ConfigureAwait(false);
            if (original != null)
            {
                throw new FormatException($"Данный {nameof(friend.Id)} \"{friend.Id}\" уже занят");
            }

            var entity = new Friend {Id = friend.Id}.Extend(friend);
            var res = await _friendProvider.SaveFriendAsync(entity).ConfigureAwait(false);
            return res;
        }

        public async Task<Friend> UpdateFriendAsync(Friend friend)
        {
            friend.EnsureIsValid();
            var original = await _friendProvider.GetFriendOrDefaultAsync(friend.Id).ConfigureAwait(false);
            var res = await _friendProvider.SaveFriendAsync(original.Extend(friend)).ConfigureAwait(false);
            return res;
        }
    }
}