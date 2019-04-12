using System.Collections.Generic;
using System.Threading.Tasks;
using DevActivator.Meetups.BL.Models;

namespace DevActivator.Meetups.BL.Interfaces
{
    public interface IFriendService
    {
        Task<List<AutocompleteRow>> GetAllFriendsAsync();

        Task<FriendVm> GetFriendAsync(string friendId);

        Task<FriendVm> AddFriendAsync(FriendVm friend);

        Task<FriendVm> UpdateFriendAsync(FriendVm friend);
    }
}