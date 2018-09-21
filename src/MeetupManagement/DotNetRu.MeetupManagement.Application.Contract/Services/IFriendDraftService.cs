using System.Collections.Generic;
using DotNetRu.MeetupManagement.Application.Contract.Models;

namespace DotNetRu.MeetupManagement.Application.Contract.Services
{
    public interface IFriendDraftService
    {
        FriendDraft CreateFriendDraft(CreateFriendDraftParameters parameters);
        void UpdateFriendDraft(FriendDraft friendDraft);
        void DeleteFriendDraft(string friendDraftId);
        ICollection<MeetupDraft> GetMeetups(string friendDraftId);
    }
}
