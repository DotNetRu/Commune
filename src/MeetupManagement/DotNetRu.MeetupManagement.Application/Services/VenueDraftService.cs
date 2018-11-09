using System;
using System.Collections.Generic;
using System.Text;
using DotNetRu.MeetupManagement.Application.Contract.Models;
using DotNetRu.MeetupManagement.Application.Contract.Services;

namespace DotNetRu.MeetupManagement.Application.Services
{
    public class VenueDraftService : IVenueDraftService
    {
        public VenueDraft CreateVenueDraft(string communityId, CreateVenueDraftParameters parameters)
        {
            throw new NotImplementedException();
        }

        public void DeleteVenueDraft(string communityId, string venueDraftId)
        {
            throw new NotImplementedException();
        }

        public ICollection<MeetupDraft> GetMeetups(string communityId, string venueDraftId)
        {
            throw new NotImplementedException();
        }

        public void UpdateVenueDraft(string communityId, VenueDraft venueDraft)
        {
            throw new NotImplementedException();
        }
    }
}
