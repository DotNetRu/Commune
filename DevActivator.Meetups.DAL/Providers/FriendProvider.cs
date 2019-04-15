using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevActivator.Common.BL.Config;
using DevActivator.Common.DAL;
using DevActivator.Meetups.BL.Entities;
using DevActivator.Meetups.BL.Interfaces;
using DevActivator.Meetups.DAL.Config;
using DevActivator.Meetups.DAL.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DevActivator.Meetups.DAL.Providers
{
    public class FriendProvider : IFriendProvider
    {
        private readonly DotNetRuServerContext _context;

        public FriendProvider(DotNetRuServerContext context)
        {
            _context = context;
        }

        public Task<List<Friend>> GetAllFriendsAsync()
            => _context.Friends.OrderBy(x => x.Id).ToListAsync();

        public Task<Friend> GetFriendOrDefaultAsync(string friendId)
            => _context.Friends.FirstOrDefaultAsync(x => x.ExportId == friendId);

        public async Task<Friend> SaveFriendAsync(Friend friend)
        {
            await _context.Friends.AddAsync(friend);
            await _context.SaveChangesAsync();
            return friend;
        }
    }
}