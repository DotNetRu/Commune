using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetRuServer.Meetups.BL.Interfaces;
using DotNetRuServer.Meetups.BL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DotNetRuServer.Controllers
{
    [Route("api/[controller]")]
    public class FriendController : BaseController
    {
        private readonly IFriendService _friendService;
        private readonly IImageService _imageService;

        public FriendController(IFriendService friendService, IImageService imageService, ILoggerFactory logger) :
            base(logger)
        {
            _friendService = friendService;
            _imageService = imageService;
        }

        [HttpGet("[action]")]
        public Task<List<AutocompleteRow>> GetFriends()
        {
            try
            {
                LogMethodBegin();

                Task<List<AutocompleteRow>> result = _friendService.GetAllFriendsAsync();

                LogMethodEnd(result);

                return result;
            }
            catch (Exception e)
            {
                LogMethodError(e);
                throw;
            }
        }

        [HttpGet("[action]/{friendId}")]
        public Task<FriendVm> GetFriend(string friendId)
        {
            try
            {
                LogMethodBegin(friendId);

                Task<FriendVm> result = _friendService.GetFriendAsync(friendId);

                LogMethodEnd(result);

                return result;
            }
            catch (Exception e)
            {
                LogMethodError(e);
                throw;
            }
        }

        [HttpPost("[action]")]
        public Task<FriendVm> AddFriend([FromBody] FriendVm friend)
        {
            try
            {
                LogMethodBegin(friend);

                Task<FriendVm> result = _friendService.AddFriendAsync(friend);

                LogMethodEnd(result);

                return result;
            }
            catch (Exception e)
            {
                LogMethodError(e);
                throw;
            }
        }

        [HttpPost("[action]")]
        public Task<FriendVm> UpdateFriend([FromBody] FriendVm friend)
        {
            try
            {
                LogMethodBegin(friend);

                Task<FriendVm> result = _friendService.UpdateFriendAsync(friend);

                LogMethodEnd(result);

                return result;
            }
            catch (Exception e)
            {
                LogMethodError(e);
                throw;
            }
        }

        [HttpGet("[action]/{friendId}/fullLogo")]
        public Task<ActionResult> GetFriendFullLogo(string friendId)
        {
            try
            {
                LogMethodBegin(friendId);

                Task<ActionResult> result = GetFriendLogoAsync(friendId, ImageSize.Full);

                LogMethodEnd(result);

                return result;
            }
            catch (Exception e)
            {
                LogMethodError(e);
                throw;
            }
        } 

        [HttpGet("[action]/{friendId}/smallLogo")]
        public Task<ActionResult> GetFriendSmallLogo(string friendId)
        {
            try
            {
                LogMethodBegin(friendId);

                Task<ActionResult> result = GetFriendLogoAsync(friendId, ImageSize.Small);

                LogMethodEnd(result);

                return result;
            }
            catch (Exception e)
            {
                LogMethodError(e);
                throw;
            }
        }

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