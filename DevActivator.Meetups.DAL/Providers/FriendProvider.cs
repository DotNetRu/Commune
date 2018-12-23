using System.Collections.Generic;
using System.Threading.Tasks;
using DevActivator.Common.BL.Config;
using DevActivator.Common.DAL;
using DevActivator.Meetups.BL.Entities;
using DevActivator.Meetups.BL.Interfaces;
using DevActivator.Meetups.DAL.Config;
using Microsoft.Extensions.Logging;

namespace DevActivator.Meetups.DAL.Providers
{
    public class FriendProvider : BaseProvider<Friend>, IFriendProvider
    {
        public FriendProvider(ILogger<FriendProvider> l, Settings s) : base(l, s, FriendConfig.DirectoryName)
        {
        }

        public Task<List<Friend>> GetAllFriendsAsync()
            => GetAllAsync();

        public Task<Friend> GetFriendOrDefaultAsync(string friendId)
            => GetEntityByIdAsync(friendId);

        public Task<Friend> SaveFriendAsync(Friend friend)
            => SaveEntityAsync(friend);
    }
}