using System.Collections.Generic;
using System.Threading.Tasks;
using DevActivator.Meetup.BL.Models;

namespace DevActivator.Meetup.BL.Interfaces
{
    public interface ITalkService
    {
        Task<List<AutocompleteRow>> GetAllTalksAsync();

        Task<TalkVm> GetTalkAsync(string talkId);

        Task<TalkVm> AddTalkAsync(TalkVm talk);

        Task<TalkVm> UpdateTalkAsync(TalkVm talk);
    }
}