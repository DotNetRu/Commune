using System.Collections.Generic;
using System.Threading.Tasks;
using DevActivator.Meetups.BL.Entities;

namespace DevActivator.Meetups.BL.Interfaces
{
    public interface ITalkProvider
    {
        Task<List<Talk>> GetAllTalksAsync();

        Task<Talk> GetTalkOrDefaultAsync(string talkId);

        Task<Talk> SaveTalkAsync(Talk talk);
    }
}