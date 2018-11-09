using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using DotNetRu.MeetupManagement.Application.Contract.Services;
using DotNetRu.MeetupManagement.Application.Services;
using DotNetRu.MeetupManagement.Domain.Contract.Exceptions;
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
            if (ValidateRequest(new List<string> { communityId, meetupDraft.Id, meetupDraft.Name }))
            {
                try
                {
                    var meetUp = _meetupDraftService.CreateMeetupDraft(communityId, meetupDraft.Name);
                    this.HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;
                }
                catch (CommunityNotFoundException)
                {
                    this.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                }
            }

            // ToDo: my guess, we need to return meetup id at least
        }

        public override void DeleteMeetupDraft([FromRoute, Required] string communityId, [FromRoute, Required] string meetupId)
        {
            if (ValidateRequest(new List<string> { communityId, meetupId }))
            {
                try
                {
                    _meetupDraftService.DeleteMeetupDraft(communityId, meetupId);
                    this.HttpContext.Response.StatusCode = (int)HttpStatusCode.NoContent;
                }
                catch (Exception ex)
                {
                    if (ex is CommunityNotFoundException || ex is MeetupNotFoundException)
                    {
                        this.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }

        public override Contract.Models.MeetupDraft GetMeetupDraft([FromRoute, Required] string communityId, [FromRoute, Required] string meetupId)
        {
            if (ValidateRequest(new List<string> { communityId, meetupId }))
            {
                try
                {
                    var result = _meetupDraftService.GetMeetupDraft(communityId, meetupId);
                    this.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                    return Map(result);
                }
                catch (Exception ex)
                {
                    if (ex is CommunityNotFoundException || ex is MeetupNotFoundException)
                    {
                        this.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return null;
        }

        public override void UpdateMeetupDraft([FromRoute, Required] string communityId, [FromRoute, Required] string meetupId, [FromBody] UpdateMeetupDraftParameters updateMeetupDraftProperties)
        {
            if (ValidateRequest(new List<string> { communityId, meetupId }))
            {
                try
                {
                    _meetupDraftService.UpdateMeetupDraft(communityId, meetupId, updateMeetupDraftProperties.TalkIds, updateMeetupDraftProperties.SpeakerIds, updateMeetupDraftProperties.VenueId);
                    this.HttpContext.Response.StatusCode = (int)HttpStatusCode.NoContent;
                }
                catch (Exception ex)
                {
                    if (ex is CommunityNotFoundException || ex is MeetupNotFoundException)
                    {
                        this.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    }
                    else
                    {
                        throw;
                    }
                }
            }
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
