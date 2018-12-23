using System.Collections.Generic;
using System.Threading.Tasks;
using DevActivator.Meetups.BL.Models;

namespace DevActivator.Meetups.BL.Interfaces
{
    public interface IMeetupService
    {
        Task<List<AutocompleteRow>> GetAllMeetupsAsync();

        Task<MeetupVm> GetMeetupAsync(string meetupId);

        Task<MeetupVm> AddMeetupAsync(MeetupVm meetup);

        Task<MeetupVm> UpdateMeetupAsync(MeetupVm meetup);
    }
}