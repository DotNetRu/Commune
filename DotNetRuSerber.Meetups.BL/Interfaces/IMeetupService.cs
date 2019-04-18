using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetRuSerber.Meetups.BL.Models;

namespace DotNetRuSerber.Meetups.BL.Interfaces
{
    public interface IMeetupService
    {
        Task<List<AutocompleteRow>> GetAllMeetupsAsync();

        Task<MeetupVm> GetMeetupAsync(string meetupId);

        Task<MeetupVm> AddMeetupAsync(MeetupVm meetup);

        Task<MeetupVm> UpdateMeetupAsync(MeetupVm meetup);
    }
}