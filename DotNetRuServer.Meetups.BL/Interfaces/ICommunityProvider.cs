using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetRuServer.Meetups.BL.Entities;

namespace DotNetRuServer.Meetups.BL.Interfaces
{
    public interface ICommunityProvider
    {
        Task<List<Community>> GetAllCommunitiesAsync();

        Task<Community> GetCommunityOrDefaultAsync(string communityId);

        Task<Community> SaveCommunityAsync(Community community);
    }
}