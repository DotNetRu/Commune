using System.Collections.Generic;
using System.Threading.Tasks;
using DevActivator.Meetup.BL.Entities;
using DevActivator.Meetup.BL.Models;

namespace DevActivator.Meetup.BL.Interfaces
{
    public interface IFriendService
    {
        Task<List<AutocompleteRow>> GetAllFriendsAsync();

        Task<Friend> GetFriendAsync(string friendId);

        Task<Friend> AddFriendAsync(Friend friend);

        Task<Friend> UpdateFriendAsync(Friend friend);
    }
}