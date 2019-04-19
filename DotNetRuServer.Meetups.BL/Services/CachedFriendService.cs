using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetRuServer.Comon.BL.Caching;
using DotNetRuServer.Meetups.BL.Interfaces;
using DotNetRuServer.Meetups.BL.Models;

namespace DotNetRuServer.Meetups.BL.Services
{
    public class CachedFriendService : IFriendService
    {
        private readonly ICache _cache;
        private readonly IFriendService _friendService;

        public CachedFriendService(ICache cache, IFriendService friendServiceImplementation)
        {
            _cache = cache;
            _friendService = friendServiceImplementation;
        }

        public Task<List<AutocompleteRow>> GetAllFriendsAsync()
            => _cache.GetOrCreateAsync(nameof(GetAllFriendsAsync),
                cacheEntry =>
                {
                    cacheEntry.SlidingExpiration = TimeSpan.FromMinutes(10);
                    return _friendService.GetAllFriendsAsync();
                }
            );

        public Task<FriendVm> GetFriendAsync(string friendId)
            => _friendService.GetFriendAsync(friendId);

        public async Task<FriendVm> AddFriendAsync(FriendVm friend)
        {
            var result = await _friendService.AddFriendAsync(friend).ConfigureAwait(false);

            _cache.Remove(nameof(GetAllFriendsAsync));

            return result;
        }

        public async Task<FriendVm> UpdateFriendAsync(FriendVm friend)
        {
            var result = await _friendService.UpdateFriendAsync(friend).ConfigureAwait(false);

            _cache.Remove(nameof(GetAllFriendsAsync));
            if (_cache.TryGetValue<List<AutocompleteRow>>(nameof(GetAllFriendsAsync), out var friends))
            {
                friends.ForEach(x => _cache.Remove($"{nameof(GetFriendAsync)}_{x.Id}"));
            }

            return result;
        }
    }
}