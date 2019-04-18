using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetRuSerber.Meetups.BL.Models;

namespace DotNetRuSerber.Meetups.BL.Interfaces
{
    public interface IFriendService
    {
        Task<List<AutocompleteRow>> GetAllFriendsAsync();

        Task<FriendVm> GetFriendAsync(string friendId);

        Task<FriendVm> AddFriendAsync(FriendVm friend);

        Task<FriendVm> UpdateFriendAsync(FriendVm friend);
    }
}