using DotNetRu.MeetupManagement.WebApi.Contract.Controllers;
using DotNetRu.MeetupManagement.WebApi.Contract.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;

namespace DotNetRu.MeetupManagement.WebApi.Controllers
{
    public class MeetupController : MeetupApiController
    {
        private readonly ILogger<MeetupController> _logger;

        public MeetupController(ILogger<MeetupController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override void CreateMeetupDraft([FromRoute, Required] string communityId, [FromBody] MeetupDraft meetupDraft)
        {
            throw new NotImplementedException();
        }

        public override void DeleteMeetupDraft([FromRoute, Required] string communityId, [FromRoute, Required] string meetupId)
        {
            throw new NotImplementedException();
        }

        public override MeetupDraftProperties GetMeetupDraft([FromRoute, Required] string communityId, [FromRoute, Required] string meetupId)
        {
            throw new NotImplementedException();
        }

        public override void UpdateMeetupDraft([FromRoute, Required] string communityId, [FromRoute, Required] string meetupId, [FromBody] UpdatableMeetupDraftProperties updateMeetupDraftProperties)
        {
            throw new NotImplementedException();
        }
    }
}
