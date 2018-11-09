using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using DotNetRu.MeetupManagement.Application.Contract.Services;
using DotNetRu.MeetupManagement.WebApi.Contract.Controllers;
using DotNetRu.MeetupManagement.WebApi.Contract.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DotNetRu.MeetupManagement.WebApi.Controllers
{
    public class FriendController : FriendApiController
    {
        private readonly ILogger<FriendController> _logger;
        private readonly IFriendDraftService _friendDraftService;

        public FriendController(ILogger<FriendController> logger, IFriendDraftService friendDraftService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _friendDraftService = friendDraftService ?? throw new ArgumentNullException(nameof(friendDraftService));
        }

        public override void CreateFriendDraft([FromBody] CreateFriendDraftParameters body)
        {
            throw new NotImplementedException();
        }

        public override void DeleteFriendDraft([FromRoute, Required] string friendId)
        {
            throw new NotImplementedException();
        }

        public override FriendDraft GetFriendDraft([FromRoute, Required] string friendId)
        {
            throw new NotImplementedException();
        }

        public override void UpdateFriendDraft([FromRoute, Required] string friendId, [FromBody] UpdateFriendDraftParameters body)
        {
            throw new NotImplementedException();
        }
    }
}
