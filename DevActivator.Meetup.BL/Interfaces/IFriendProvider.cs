using System.Collections.Generic;
using System.Threading.Tasks;
using DevActivator.Meetup.BL.Entities;

namespace DevActivator.Meetup.BL.Interfaces
{
    public interface IFriendProvider
    {
        Task<List<Friend>> GetAllFriendsAsync();

        Task<Friend> GetFriendOrDefaultAsync(string friendId);

        Task<Friend> SaveFriendAsync(Friend friend);
    }
}