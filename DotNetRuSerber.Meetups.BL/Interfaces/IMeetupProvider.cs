using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetRuSerber.Meetups.BL.Entities;

namespace DotNetRuSerber.Meetups.BL.Interfaces
{
    public interface IMeetupProvider
    {
        Task<List<Meetup>> GetAllMeetupsAsync();

        Task<Meetup> GetMeetupOrDefaultAsync(string meetupId);
        Task<Meetup> GetMeetupOrDefaultExtendedAsync(string meetupId);

        Task<Meetup> SaveMeetupAsync(Meetup meetup);
    }
}