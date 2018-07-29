using System.Collections.Generic;
using DotNetRu.MeetupManagement.Application.Contract.Models;

namespace DotNetRu.MeetupManagement.Application.Contract.Services
{
    public interface IVenueDraftService
    {
        VenueDraft CreateVenueDraft(string comminityId, CreateVenueDraftParameters parameters);
        void UpdateVenueDraft(string comminityId, VenueDraft venueDraft);
        void DeleteVenueDraft(string comminityId, string venueDraftId);
        ICollection<MeetupDraft> GetMeetups(string comminityId, string venueDraftId);
    }
}
