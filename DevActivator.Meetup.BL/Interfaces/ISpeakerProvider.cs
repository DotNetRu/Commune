using System.Collections.Generic;
using System.Threading.Tasks;
using DevActivator.Meetup.BL.Entities;

namespace DevActivator.Meetup.BL.Interfaces
{
    public interface ISpeakerProvider
    {
        Task<List<Speaker>> GetAllSpeakersAsync();

        Task<Speaker> GetSpeakerOrDefaultAsync(string speakerId);

        Task<Speaker> SaveSpeakerAsync(Speaker speaker);
    }
}