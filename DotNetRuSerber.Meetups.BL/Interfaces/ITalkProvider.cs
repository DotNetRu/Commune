using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetRuSerber.Meetups.BL.Entities;

namespace DotNetRuSerber.Meetups.BL.Interfaces
{
    public interface ITalkProvider
    {
        Task<List<Talk>> GetAllTalksAsync();

        Task<Talk> GetTalkOrDefaultAsync(string talkId);
        Task<Talk> GetTalkOrDefaultExtendedAsync(string talkId);

        Task<Talk> SaveTalkAsync(Talk talk);
    }
}