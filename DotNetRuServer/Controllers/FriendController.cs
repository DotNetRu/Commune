using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetRuServer.Meetups.BL.Interfaces;
using DotNetRuServer.Meetups.BL.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetRuServer.Controllers
{
    [Route("api/[controller]")]
    public class FriendController : Controller
    {
        private readonly IFriendService _friendService;
        private readonly IImageService _imageService;

        public FriendController(IFriendService friendService, IImageService imageService)
        {
            _friendService = friendService;
            _imageService = imageService;
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

        [HttpGet("[action]/{friendId}/fullLogo")]
        public Task<ActionResult> GetFriendFullLogo(string friendId)
            => GetFriendLogoAsync(friendId, ImageSize.Full);

        [HttpGet("[action]/{friendId}/smallLogo")]
        public Task<ActionResult> GetFriendSmallLogo(string friendId)
            => GetFriendLogoAsync(friendId, ImageSize.Small);

        private async Task<ActionResult> GetFriendLogoAsync(string friendId, ImageSize imageSize)
        {
            var image = await _imageService.GetFriendLogoOrDefaultAsync(friendId, imageSize);
            if (image == null)
            {
                return NotFound();
            }

            return new FileContentResult(image.Data, image.MimeType);
        }
    }
}