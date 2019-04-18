using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetRuServer.Meetups.BL.Entities;
using DotNetRuServer.Meetups.BL.Interfaces;
using DotNetRuServer.Meetups.DAL.Database;
using Microsoft.EntityFrameworkCore;

namespace DotNetRuServer.Meetups.DAL.Providers
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