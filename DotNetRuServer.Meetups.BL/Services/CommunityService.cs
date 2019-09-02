using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetRuServer.Meetups.BL.Extensions;
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

        public async Task<CommunityVm> GetCommunityAsync(string communityId)
        {
            var community = await _provider.GetCommunityOrDefaultAsync(communityId);
            return community.ToVm();
        }

        public Task<CommunityVm> AddCommunityAsync(CommunityVm community)
        {
            throw new NotImplementedException();
        }

        public Task<CommunityVm> UpdateCommunityAsync(CommunityVm community)
        {
            throw new NotImplementedException();
        }
    }
}