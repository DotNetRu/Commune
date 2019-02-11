using DotNetRu.MeetupManagement.WebApi.Contract.Controllers;
using DotNetRu.MeetupManagement.WebApi.Contract.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetRu.MeetupManagement.WebApi.Controllers
{
    public class SpeakerController : SpeakerApiController
    {
        public override ActionResult<SpeakerDraft> CreateSpeakerDraft(CreateSpeakerDraftParameters speakerDraft) => throw new System.NotImplementedException();

        public override EmptyResult DeleteSpeakerDraft(string speakerId) => throw new System.NotImplementedException();

        public override ActionResult<SpeakerDraft> GetSpeakerDrafts(string speakerId) => throw new System.NotImplementedException();

        public override EmptyResult UpdateSpeakerDraft(string speakerId, UpdateSpeakerDraftParameters body) => throw new System.NotImplementedException();
    }
}
