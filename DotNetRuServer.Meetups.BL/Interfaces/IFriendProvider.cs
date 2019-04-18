using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetRuServer.Meetups.BL.Entities;

namespace DotNetRuServer.Meetups.BL.Interfaces
{
    public interface IFriendProvider
    {
        Task<List<Friend>> GetAllFriendsAsync();

        Task<Friend> GetFriendOrDefaultAsync(string friendId);

        Task<Friend> SaveFriendAsync(Friend friend);
    }
}