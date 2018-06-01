using System;
using System.ComponentModel.DataAnnotations;
using DotNetRu.MeetupManagement.WebApi.Contract.Controllers;
using DotNetRu.MeetupManagement.WebApi.Contract.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DotNetRu.MeetupManagement.WebApi.Controllers
{
    public class MeetupController : MeetupApiController
    {
        private ILogger<MeetupController> _logger;

        // ReSharper disable once UnusedMember.Global
        public MeetupController(ILogger<MeetupController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override void CreateMeetupDraft([FromRoute, Required] string communityId, [FromBody] CreateMeetupDraftParameters meetupDraft)
        {
            throw new NotImplementedException();
        }

        public override void DeleteMeetupDraft([FromRoute, Required] string communityId, [FromRoute, Required] string meetupId)
        {
            throw new NotImplementedException();
        }

        public override MeetupDraft GetMeetupDraft([FromRoute, Required] string communityId, [FromRoute, Required] string meetupId)
        {
            throw new NotImplementedException();
        }

        public override void UpdateMeetupDraft([FromRoute, Required] string communityId, [FromRoute, Required] string meetupId, [FromBody] UpdateMeetupDraftParameters updateMeetupDraftProperties)
        {
            throw new NotImplementedException();
        }
    }
}
