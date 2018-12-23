using System.Collections.Generic;
using System.Threading.Tasks;
using DevActivator.Meetups.BL.Entities;
using DevActivator.Meetups.BL.Models;

namespace DevActivator.Meetups.BL.Interfaces
{
    public interface IFriendService
    {
        Task<List<AutocompleteRow>> GetAllFriendsAsync();

        Task<Friend> GetFriendAsync(string friendId);

        Task<Friend> AddFriendAsync(Friend friend);

        Task<Friend> UpdateFriendAsync(Friend friend);
    }
}