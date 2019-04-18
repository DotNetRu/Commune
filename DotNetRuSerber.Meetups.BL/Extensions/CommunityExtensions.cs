using DotNetRuSerber.Meetups.BL.Entities;
using DotNetRuSerber.Meetups.BL.Models;

namespace DotNetRuSerber.Meetups.BL.Extensions
{
    public static class CommunityExtensions
    {
        public static CommunityVm ToVm(this Community community)
            => new CommunityVm
            {
                Id = community.ExportId,
                Name = community.Name,
                City = community.City,
                TimeZone = community.TimeZone
            };

        public static Community Extend(this Community original, CommunityVm vm)
            => new Community
            {
                Id = original.Id,
                Name = vm.Name,
                City = vm.City,
                TimeZone = vm.TimeZone,
            };
    }
}