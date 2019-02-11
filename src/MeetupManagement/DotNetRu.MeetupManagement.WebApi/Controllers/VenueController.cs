using DotNetRu.MeetupManagement.WebApi.Contract.Controllers;
using DotNetRu.MeetupManagement.WebApi.Contract.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetRu.MeetupManagement.WebApi.Controllers
{
    public class VenueController : VenueApiController
    {
        public override ActionResult<VenueDraft> CreateVenueDraft(CreateVenueDraftParameters venueDraft) => throw new System.NotImplementedException();

        public override EmptyResult DeleteVenueDraft(string venueId) => throw new System.NotImplementedException();

        public override ActionResult<VenueDraft> GetVenueDrafts(string venueId) => throw new System.NotImplementedException();

        public override EmptyResult UpdateVenueDraft(string venueId, UpdateVenueDraftParameters properties) => throw new System.NotImplementedException();
    }
}
