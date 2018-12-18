using System.Collections.Generic;
using System.Threading.Tasks;
using DevActivator.Meetup.BL.Entities;

namespace DevActivator.Meetup.BL.Interfaces
{
    public interface ITalkProvider
    {
        Task<List<Talk>> GetAllTalksAsync();

        Task<Talk> GetTalkOrDefaultAsync(string talkId);

        Task<Talk> SaveTalkAsync(Talk talk);
    }
}