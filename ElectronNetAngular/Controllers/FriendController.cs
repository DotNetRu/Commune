using System.Collections.Generic;
using System.Threading.Tasks;
using DevActivator.Meetup.BL.Entities;
using DevActivator.Meetup.BL.Interfaces;
using DevActivator.Meetup.BL.Models;
using Microsoft.AspNetCore.Mvc;

namespace ElectronNetAngular.Controllers
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
        public Task<Friend> GetFriend(string friendId)
            => _friendService.GetFriendAsync(friendId);

        [HttpPost("[action]")]
        public Task<Friend> AddFriend([FromBody] Friend friend)
            => _friendService.AddFriendAsync(friend);

        [HttpPost("[action]")]
        public Task<Friend> UpdateFriend([FromBody] Friend friend)
            => _friendService.UpdateFriendAsync(friend);
    }
}