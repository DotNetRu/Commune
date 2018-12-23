using System.Collections.Generic;
using System.Threading.Tasks;
using DevActivator.Meetups.BL.Entities;

namespace DevActivator.Meetups.BL.Interfaces
{
    public interface IMeetupProvider
    {
        Task<List<Meetup>> GetAllMeetupsAsync();

        Task<Meetup> GetMeetupOrDefaultAsync(string meetupId);

        Task<Meetup> SaveMeetupAsync(Meetup meetup);
    }
}