using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetRuServer.Meetups.BL.Entities;

namespace DotNetRuServer.Meetups.BL.Interfaces
{
    public interface ITalkProvider
    {
        Task<List<Talk>> GetAllTalksAsync();

        Task<Talk> GetTalkOrDefaultAsync(string talkId);
        Task<Talk> GetTalkOrDefaultExtendedAsync(string talkId);

        Task<Talk> SaveTalkAsync(Talk talk);
        void RemoveSpeaker(Talk talk, int speakerId);
    }
}