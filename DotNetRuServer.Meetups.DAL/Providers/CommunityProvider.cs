using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevActivator.Meetups.BL.Entities;
using DevActivator.Meetups.BL.Interfaces;
using DotNetRuServer.Meetups.DAL.Database;
using Microsoft.EntityFrameworkCore;

namespace DotNetRuServer.Meetups.DAL.Providers
{
    public class CommunityProvider : ICommunityProvider
    {
        private readonly DotNetRuServerContext _context;

        public CommunityProvider(DotNetRuServerContext context)
        {
            _context = context;
        }

        public Task<List<Community>> GetAllCommunitiesAsync()
            => _context.Communities.OrderBy(x => x.Id).ToListAsync();

        public Task<Community> GetCommunityOrDefaultAsync(string communityId)
            => _context.Communities.FirstOrDefaultAsync(x => x.ExportId == communityId);

        public async Task<Community> SaveCommunityAsync(Community community)
        {
            await _context.Communities.AddAsync(community);
            await _context.SaveChangesAsync();
            return community;
        }
    }
}