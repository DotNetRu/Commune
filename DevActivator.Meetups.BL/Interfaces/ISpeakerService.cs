using System.Collections.Generic;
using System.Threading.Tasks;
using DevActivator.Meetups.BL.Models;

namespace DevActivator.Meetups.BL.Interfaces
{
    public interface ISpeakerService
    {
        Task<List<AutocompleteRow>> GetAllSpeakersAsync();

        Task<SpeakerVm> GetSpeakerAsync(string speakerId);

        Task<SpeakerVm> AddSpeakerAsync(SpeakerVm speaker);

        Task<SpeakerVm> UpdateSpeakerAsync(SpeakerVm speaker);
    }
}