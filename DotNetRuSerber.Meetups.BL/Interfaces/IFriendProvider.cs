using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetRuSerber.Meetups.BL.Entities;

namespace DotNetRuSerber.Meetups.BL.Interfaces
{
    public interface IFriendProvider
    {
        Task<List<Friend>> GetAllFriendsAsync();

        Task<Friend> GetFriendOrDefaultAsync(string friendId);

        Task<Friend> SaveFriendAsync(Friend friend);
    }
}