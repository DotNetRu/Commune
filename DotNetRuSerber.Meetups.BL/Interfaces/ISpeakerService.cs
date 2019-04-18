using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetRuSerber.Meetups.BL.Models;

namespace DotNetRuSerber.Meetups.BL.Interfaces
{
    public interface ISpeakerService
    {
        Task<List<AutocompleteRow>> GetAllSpeakersAsync();

        Task<SpeakerVm> GetSpeakerAsync(string speakerId);

        Task<SpeakerVm> AddSpeakerAsync(SpeakerVm speaker);

        Task<SpeakerVm> UpdateSpeakerAsync(SpeakerVm speaker);
    }
}