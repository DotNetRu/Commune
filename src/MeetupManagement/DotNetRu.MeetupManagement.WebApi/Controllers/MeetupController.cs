using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using DotNetRu.MeetupManagement.Application.Contract.Services;
using DotNetRu.MeetupManagement.WebApi.Contract.Controllers;
using DotNetRu.MeetupManagement.WebApi.Contract.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FriendReference = DotNetRu.MeetupManagement.WebApi.Contract.Models.FriendReference;

namespace DotNetRu.MeetupManagement.WebApi.Controllers
{
    public class MeetupController : MeetupApiController
    {
        private readonly ILogger<MeetupController> _logger;
        private readonly IMeetupDraftService _meetupDraftService;

        public MeetupController(ILogger<MeetupController> logger, IMeetupDraftService meetupDraftService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _meetupDraftService = meetupDraftService ?? throw new ArgumentNullException(nameof(meetupDraftService));
        }

        public override void CreateMeetupDraft([FromRoute, Required] string communityId, [FromBody] CreateMeetupDraftParameters meetupDraft)
        {
            var meetUp = _meetupDraftService.CreateMeetupDraft(communityId, meetupDraft.Name);

            // ToDo: my guess, we need to return meetup id at least
        }

        public override void DeleteMeetupDraft([FromRoute, Required] string communityId, [FromRoute, Required] string meetupId)
        {
            _meetupDraftService.DeleteMeetupDraft(communityId, meetupId);
        }

        public override Contract.Models.MeetupDraft GetMeetupDraft([FromRoute, Required] string communityId, [FromRoute, Required] string meetupId)
        {
            var result = _meetupDraftService.GetMeetupDraft(communityId, meetupId);
            return Map(result);
        }

        public override void UpdateMeetupDraft([FromRoute, Required] string communityId, [FromRoute, Required] string meetupId, [FromBody] UpdateMeetupDraftParameters updateMeetupDraftProperties)
        {
            _meetupDraftService.UpdateMeetupDraft(communityId, meetupId, updateMeetupDraftProperties.TalkIds, updateMeetupDraftProperties.SpeakerIds, updateMeetupDraftProperties.VenueId);
        }

        private Contract.Models.MeetupDraft Map(Application.Contract.Models.MeetupDraft source)
        {
            // ToDo: name and speakers are missing here
            var destination = new Contract.Models.MeetupDraft { Id = source.Id, Venue = Map(source.Venue) }; // , Friends = new Collection<FriendReference>(), Talks = new Collection<TalkReference>() };
            source.Friends.Select(f =>
            {
                var mapped = Map(f);
                destination.Friends.Add(mapped);
                return mapped;
            });
            source.Talks.Select(talk =>
            {
                var mapped = Map(talk);
                destination.Talks.Add(mapped);
                return mapped;
            });
            return destination;
        }

        private Contract.Models.FriendReference Map(Application.Contract.Models.FriendReference source)
        {
            return new FriendReference { Id = source.Id, Name = source.Name };
        }

        private Contract.Models.VenueReference Map(Application.Contract.Models.VenueReference source)
        {
            return new Contract.Models.VenueReference { Name = source.Name, Id = source.Id };
        }

        private TalkReference Map(Application.Contract.Models.TalkDraft source)
        {
            return new TalkReference { Id = source.Id, Title = source.Title };
        }
    }
}
