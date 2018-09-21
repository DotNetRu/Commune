using System.Collections.Generic;
using DotNetRu.MeetupManagement.Application.Contract.Models;

namespace DotNetRu.MeetupManagement.Application.Contract.Services
{
    public interface IVenueDraftService
    {
        VenueDraft CreateVenueDraft(string communityId, CreateVenueDraftParameters parameters);
        void UpdateVenueDraft(string communityId, VenueDraft venueDraft);
        void DeleteVenueDraft(string communityId, string venueDraftId);
        ICollection<MeetupDraft> GetMeetups(string communityId, string venueDraftId);
    }
}
