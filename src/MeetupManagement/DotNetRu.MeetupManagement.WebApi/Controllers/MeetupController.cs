using System;
using System.ComponentModel.DataAnnotations;
using DotNetRu.MeetupManagement.Application.Contract.Services;
using DotNetRu.MeetupManagement.WebApi.Contract.Controllers;
using DotNetRu.MeetupManagement.WebApi.Contract.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DotNetRu.MeetupManagement.WebApi.Controllers
{
    public class MeetupController : MeetupApiController
    {
        private readonly ILogger<MeetupController> _logger;

        // ReSharper disable once UnusedMember.Global
        public MeetupController(ILogger<MeetupController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override ActionResult<MeetupDraft> CreateMeetupDraft([FromRoute, Required] string communityId, [FromBody] CreateMeetupDraftParameters meetupDraft)
        {
            // var result = _meetupDraftService.CreateMeetupDraft(communityId, meetupDraft.Name);
            throw new NotImplementedException();
        }

        public override EmptyResult DeleteMeetupDraft([FromRoute, Required] string communityId, [FromRoute, Required] string meetupId)
        {
            throw new NotImplementedException();
        }

        public override ActionResult<MeetupDraft> GetMeetupDraft([FromRoute, Required] string communityId, [FromRoute, Required] string meetupId)
        {
            throw new NotImplementedException();
        }

        public override EmptyResult UpdateMeetupDraft([FromRoute, Required] string communityId, [FromRoute, Required] string meetupId, [FromBody] UpdateMeetupDraftParameters updateMeetupDraftProperties)
        {
            throw new NotImplementedException();
        }
    }
}
