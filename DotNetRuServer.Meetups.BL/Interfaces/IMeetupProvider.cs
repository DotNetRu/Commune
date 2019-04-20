using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetRuServer.Meetups.BL.Entities;

namespace DotNetRuServer.Meetups.BL.Interfaces
{
    public interface IMeetupProvider
    {
        Task<List<Meetup>> GetAllMeetupsAsync();

        Task<Meetup> GetMeetupOrDefaultAsync(string meetupId);
        Task<Meetup> GetMeetupOrDefaultExtendedAsync(string meetupId);

        Task<Meetup> SaveMeetupAsync(Meetup meetup);
    }
}