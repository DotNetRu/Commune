using System;
using System.Collections.ObjectModel;
using System.Linq;
using DotNetRu.MeetupManagement.Application.Contract.Services;
using DotNetRu.MeetupManagement.Domain.Contract.Exceptions;
using DotNetRu.MeetupManagement.WebApi.Contract.Controllers;
using DotNetRu.MeetupManagement.WebApi.Contract.Models;
using Microsoft.AspNetCore.Mvc;
using static System.FormattableString;

namespace DotNetRu.MeetupManagement.WebApi.Controllers
{
    public class TalkController : TalkApiController
    {
        private readonly ITalkDraftService _talkDraftService;

        public TalkController(ITalkDraftService talkDraftService)
        {
            _talkDraftService = talkDraftService ?? throw new ArgumentNullException(nameof(talkDraftService));
        }

        public override ActionResult<TalkDraft> CreateTalkDraft(string communityId, CreateTalkDraftParameters talkDraft)
        {
            try
            {
                var result = _talkDraftService.CreateTalkDraft(communityId, talkDraft.Id, talkDraft.Title, talkDraft.Description);
                return this.GetCreatedActionResult(
                    nameof(CreateTalkDraft),
                    GetCreateTalkDraftRouteValues(communityId),
                    Convert(result));
            }
            catch (CommunityNotFoundException ex)
            {
                return NotFound(Invariant($"Cannot find community {ex.CommunityId}."));
            }
            catch (TalkExistsException ex)
            {
                return Conflict(Invariant($"Talk draft '{ex.TalkId}' is already exists."));
            }
        }

        public override EmptyResult DeleteTalkDraft(string communityId, string talkId) => throw new NotImplementedException();

        public override EmptyResult DeleteTalkRehearsal(string communityId, string talkId, string talkRehearsalId) => throw new NotImplementedException();

        public override ActionResult<TalkDraft> GetTalkDraft(string communityId, string talkId) => throw new NotImplementedException();

        public override ActionResult<TalkRehearsal> GetTalkRehearsal(string communityId, string talkId, string talkRehearsalId) => throw new NotImplementedException();

        public override EmptyResult UpdateTalkDraft(string communityId, string talkId, UpdateTalkDraftParameters body) => throw new NotImplementedException();

        public override EmptyResult UpdateTalkRehearsal(
            string communityId,
            string talkId,
            string talkRehearsalId,
            UpdateTalkRehearsalParameters parameters)
        {
            throw new NotImplementedException();
        }

        private static TalkDraft Convert(Application.Contract.Models.TalkDraft talkDraft)
        {
            return new TalkDraft()
            {
                Id = talkDraft.Id,
                Title = talkDraft.Title,
                Description = talkDraft.Description,
                MeetupDraft = Convert(talkDraft.MeetupDraft),
                Speakers = new Collection<SpeakerReference>(talkDraft.Speakers.Select(Convert).ToList()),
                TalkRehearsals = new Collection<TalkRehearsalReference>(talkDraft.Rehearsals.Select(Convert).ToList())
            };
        }

        private static MeetupReference Convert(Application.Contract.Models.MeetupReference meetupDraft)
        {
            return new MeetupReference()
            {
                CommunityId = meetupDraft.CommunityId,
                Id = meetupDraft.Id,
                Name = meetupDraft.Name
            };
        }

        private static SpeakerReference Convert(Application.Contract.Models.SpeakerReference speakerReference)
        {
            return new SpeakerReference()
            {
                Id = speakerReference.Id,
                FirstName = speakerReference.FirstName,
                LastName = speakerReference.LastName
            };
        }

        private static TalkRehearsalReference Convert(Application.Contract.Models.TalkRehearsal talkRehearsal)
        {
            return new TalkRehearsalReference()
            {
                Id = talkRehearsal.Id,
                Time = talkRehearsal.Time
            };
        }
    }
}
