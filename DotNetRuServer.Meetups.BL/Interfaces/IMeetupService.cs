using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetRuServer.Meetups.BL.Models;

namespace DotNetRuServer.Meetups.BL.Interfaces
{
    public interface IMeetupService
    {
        Task<List<AutocompleteRow>> GetAllMeetupsAsync();

        Task<MeetupVm> GetMeetupAsync(string meetupId);

        Task<MeetupVm> AddMeetupAsync(MeetupVm meetup);

        Task<MeetupVm> UpdateMeetupAsync(MeetupVm meetup);
    }
}