using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetRuSerber.Meetups.BL.Models;

namespace DotNetRuSerber.Meetups.BL.Interfaces
{
    public interface ICommunityService
    {
        Task<List<AutocompleteRow>> GetAllCommunitiesAsync();

        Task<CommunityVm> GetCommunityAsync(string communityId);

        Task<CommunityVm> AddCommunityAsync(CommunityVm community);

        Task<CommunityVm> UpdateCommunityAsync(CommunityVm community);
    }
}