using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetRuSerber.Meetups.BL.Models;

namespace DotNetRuSerber.Meetups.BL.Interfaces
{
    public interface ITalkService
    {
        Task<List<AutocompleteRow>> GetAllTalksAsync();

        Task<TalkVm> GetTalkAsync(string talkId);

        Task<TalkVm> AddTalkAsync(TalkVm talk);

        Task<TalkVm> UpdateTalkAsync(TalkVm talk);
    }
}