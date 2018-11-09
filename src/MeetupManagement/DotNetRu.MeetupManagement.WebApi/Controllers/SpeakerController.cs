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
    public class SpeakerController : SpeakerApiController
    {
        private readonly ILogger<SpeakerController> _logger;
        private readonly ISpeakerDraftService _speakerDraftService;

        public SpeakerController(ILogger<SpeakerController> logger, ISpeakerDraftService speakerDraftService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _speakerDraftService = speakerDraftService ?? throw new ArgumentNullException(nameof(speakerDraftService));
        }

        public override void CreateSpeakerDraft([FromBody] CreateSpeakerDraftParameters speakerDraft)
        {
            throw new NotImplementedException();
        }

        public override void DeleteSpeakerDraft([FromRoute, Required] string speakerId)
        {
            throw new NotImplementedException();
        }

        public override SpeakerDraft GetSpeakerDrafts([FromRoute, Required] string speakerId)
        {
            throw new NotImplementedException();
        }

        public override void UpdateSpeakerDraft([FromRoute, Required] string speakerId, [FromBody] UpdateSpeakerDraftParameters body)
        {
            throw new NotImplementedException();
        }
    }
}
