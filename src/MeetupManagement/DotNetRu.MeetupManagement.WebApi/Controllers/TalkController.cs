using System;
using System.ComponentModel.DataAnnotations;
using DotNetRu.MeetupManagement.Application.Contract.Services;
using DotNetRu.MeetupManagement.WebApi.Contract.Controllers;
using DotNetRu.MeetupManagement.WebApi.Contract.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DotNetRu.MeetupManagement.WebApi.Controllers
{
    public class TalkController : TalkApiController
    {
        private readonly ILogger<TalkController> _logger;
        private readonly ITalkDraftService _talkDraftService;

        public TalkController(ILogger<TalkController> logger, ITalkDraftService talkDraftService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _talkDraftService = talkDraftService ?? throw new ArgumentNullException(nameof(talkDraftService));
        }

        public override void CreateTalkDraft([FromBody] CreateTalkDraftParameters talkDraft)
        {
            throw new NotImplementedException();
        }

        public override void DeleteTalkDraft([FromRoute, Required] string talkId)
        {
            throw new NotImplementedException();
        }

        public override TalkDraft GetTalkDraft([FromRoute, Required] string talkId)
        {
            throw new NotImplementedException();
        }

        public override void UpdateTalkDraft([FromRoute, Required] string talkId, [FromBody] UpdateTalkDraftParameters body)
        {
            throw new NotImplementedException();
        }
    }
}
