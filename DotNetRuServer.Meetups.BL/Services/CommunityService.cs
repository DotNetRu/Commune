using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetRuServer.Meetups.BL.Interfaces;
using DotNetRuServer.Meetups.BL.Models;

namespace DotNetRuServer.Meetups.BL.Services
{
    public class CommunityService : ICommunityService
    {
        private readonly ICommunityProvider _provider;

        public CommunityService(ICommunityProvider provider)
        {
            _provider = provider;
        }

        public async Task<List<AutocompleteRow>> GetAllCommunitiesAsync()
        {
            var venues = await _provider.GetAllCommunitiesAsync().ConfigureAwait(false);
            return venues
                .Select(x => new AutocompleteRow {Id = x.ExportId, Name = x.Name})
                .ToList();
        }

        public Task<CommunityVm> GetCommunityAsync(string communityId)
        {
            throw new System.NotImplementedException();
        }

        public Task<CommunityVm> AddCommunityAsync(CommunityVm community)
        {
            throw new System.NotImplementedException();
        }

        public Task<CommunityVm> UpdateCommunityAsync(CommunityVm community)
        {
            throw new System.NotImplementedException();
        }
    }
}