using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetRuSerber.Meetups.BL.Interfaces;
using DotNetRuSerber.Meetups.BL.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevActivator.Controllers
{
    [Route("api/[controller]")]
    public class FriendController : Controller
    {
        private readonly IFriendService _friendService;

        public FriendController(IFriendService friendService)
        {
            _friendService = friendService;
        }

        [HttpGet("[action]")]
        public Task<List<AutocompleteRow>> GetFriends()
            => _friendService.GetAllFriendsAsync();

        [HttpGet("[action]/{friendId}")]
        public Task<FriendVm> GetFriend(string friendId)
            => _friendService.GetFriendAsync(friendId);

        [HttpPost("[action]")]
        public Task<FriendVm> AddFriend([FromBody] FriendVm friend)
            => _friendService.AddFriendAsync(friend);

        [HttpPost("[action]")]
        public Task<FriendVm> UpdateFriend([FromBody] FriendVm friend)
            => _friendService.UpdateFriendAsync(friend);
    }
}