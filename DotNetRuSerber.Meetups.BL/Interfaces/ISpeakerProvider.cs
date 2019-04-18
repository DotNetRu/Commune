using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetRuSerber.Meetups.BL.Entities;

namespace DotNetRuSerber.Meetups.BL.Interfaces
{
    public interface ISpeakerProvider
    {
        Task<List<Speaker>> GetAllSpeakersAsync();

        Task<Speaker> GetSpeakerOrDefaultAsync(string speakerId);

        Task<List<Speaker>> GetSpeakersByIdsAsync(List<string> ids);

        Task<Speaker> SaveSpeakerAsync(Speaker speaker);
    }
}