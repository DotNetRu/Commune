using System;
using DotNetRu.MeetupManagement.WebApi.Contract.Controllers;
using DotNetRu.MeetupManagement.WebApi.Contract.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetRu.MeetupManagement.WebApi.Controllers
{
    public class FriendController : FriendApiController
    {
        public override ActionResult<FriendDraft> CreateFriendDraft(CreateFriendDraftParameters body) => throw new NotImplementedException();

        public override EmptyResult DeleteFriendDraft(string friendId) => throw new NotImplementedException();

        public override ActionResult<FriendDraft> GetFriendDraft(string friendId) => throw new NotImplementedException();

        public override EmptyResult UpdateFriendDraft(string friendId, UpdateFriendDraftParameters body) => throw new NotImplementedException();
    }
}
