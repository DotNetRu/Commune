using System.Collections.Generic;
using System.Threading.Tasks;
using DevActivator.Common.BL.Config;
using DevActivator.Common.DAL;
using DevActivator.Meetup.BL.Entities;
using DevActivator.Meetup.BL.Interfaces;
using DevActivator.Meetup.DAL.Config;
using Microsoft.Extensions.Logging;

namespace DevActivator.Meetup.DAL.Providers
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