using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetRuSerber.Meetups.BL.Entities;
using DotNetRuSerber.Meetups.BL.Extensions;
using DotNetRuSerber.Meetups.BL.Interfaces;
using DotNetRuSerber.Meetups.BL.Models;

namespace DotNetRuSerber.Meetups.BL.Services
{
    public class FriendService : IFriendService
    {
        private readonly IFriendProvider _friendProvider;
        private readonly IUnitOfWork _unitOfWork;

        public FriendService(IFriendProvider friendProvider, IUnitOfWork unitOfWork)
        {
            _friendProvider = friendProvider;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<AutocompleteRow>> GetAllFriendsAsync()
        {
            var friends = await _friendProvider.GetAllFriendsAsync().ConfigureAwait(false);
            return friends
                .Select(x => new AutocompleteRow {Id = x.ExportId, Name = x.Name})
                .ToList();
        }

        public async Task<FriendVm> GetFriendAsync(string friendId)
        {
            var friend = await _friendProvider.GetFriendOrDefaultAsync(friendId).ConfigureAwait(false);
            return friend.ToVm();
        }

        public async Task<FriendVm> AddFriendAsync(FriendVm friend)
        {
            friend.EnsureIsValid();

            var original = await _friendProvider.GetFriendOrDefaultAsync(friend.Id).ConfigureAwait(false);
            if (original != null)
            {
                throw new FormatException($"Данный {nameof(friend.Id)} \"{friend.Id}\" уже занят");
            }

            var entity = new Friend {ExportId = friend.Id}.Extend(friend);
            var res = await _friendProvider.SaveFriendAsync(entity).ConfigureAwait(false);
            return res.ToVm();
        }

        public async Task<FriendVm> UpdateFriendAsync(FriendVm friend)
        {
            friend.EnsureIsValid();
            
            var original = await _friendProvider.GetFriendOrDefaultAsync(friend.Id).ConfigureAwait(false);
            if (original == null)
            {
                throw new FormatException($"{nameof(friend.Id)} \"{friend.Id}\" нет в базе");
            }
            original.Name = friend.Name;
            original.Url = friend.Url;
            original.Description = friend.Description;

            await _unitOfWork.SaveChangesAsync();
            
            return original.ToVm();
        }
    }
}