using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetRuSerber.Meetups.BL.Entities;

namespace DotNetRuSerber.Meetups.BL.Interfaces
{
    public interface ICommunityProvider
    {
        Task<List<Community>> GetAllCommunitiesAsync();

        Task<Community> GetCommunityOrDefaultAsync(string communityId);

        Task<Community> SaveCommunityAsync(Community community);
    }
}