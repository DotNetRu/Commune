using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
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
            // Url and description should be here in params, but Id - should not
            if (ValidateRequest(new List<string> { body?.Name }))
            {
                var friend = _friendDraftService.CreateFriendDraft(new Application.Contract.Models.CreateFriendDraftParameters() { Name = body.Name });
                this.HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;
            }
        }

        public override void DeleteFriendDraft([FromRoute, Required] string friendId)
        {
            if (ValidateRequest(new List<string> { friendId }))
            {
                _friendDraftService.DeleteFriendDraft(friendId);
                this.HttpContext.Response.StatusCode = (int)HttpStatusCode.NoContent;
            }
        }

        public override FriendDraft GetFriendDraft([FromRoute, Required] string friendId)
        {
            // No such method in contract
            throw new NotImplementedException();
        }

        public override void UpdateFriendDraft([FromRoute, Required] string friendId, [FromBody] UpdateFriendDraftParameters body)
        {
            if (ValidateRequest(new List<string> { friendId, body?.Name }))
            {
                _friendDraftService.UpdateFriendDraft(new Application.Contract.Models.FriendDraft(friendId) { Description = body.Description, Name = body.Name, Url = body.Url });
                this.HttpContext.Response.StatusCode = (int)HttpStatusCode.NoContent;
            }
        }

        private bool ValidateRequest(List<string> parameters)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                this.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return false;
            }
            else if (parameters.Any(p => string.IsNullOrEmpty(p)))
            {
                this.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return false;
            }

            return true;
        }
    }
}
