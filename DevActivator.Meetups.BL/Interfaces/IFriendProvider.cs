using System.Collections.Generic;
using System.Threading.Tasks;
using DevActivator.Meetups.BL.Entities;

namespace DevActivator.Meetups.BL.Interfaces
{
    public interface IFriendProvider
    {
        Task<List<Friend>> GetAllFriendsAsync();

        Task<Friend> GetFriendOrDefaultAsync(string friendId);

        Task<Friend> SaveFriendAsync(Friend friend);
    }
}