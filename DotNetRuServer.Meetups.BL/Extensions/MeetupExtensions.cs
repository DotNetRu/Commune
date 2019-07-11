using System;
using System.Linq;
using DotNetRuServer.Meetups.BL.Entities;
using DotNetRuServer.Meetups.BL.Models;
using TimeZoneConverter;

namespace DotNetRuServer.Meetups.BL.Extensions
{
    public static class MeetupExtensions
    {
        public static MeetupVm EnsureIsValid(this MeetupVm meetup)
        {
            // todo: implement full validation
            if (string.IsNullOrWhiteSpace(meetup.Name)) throw new FormatException(nameof(meetup.Name));

            if (meetup.FriendIds == null || meetup.FriendIds.Count == 0 ||
                meetup.FriendIds.Any(x => string.IsNullOrWhiteSpace(x.FriendId)))
                throw new FormatException(nameof(meetup.FriendIds));

            if (string.IsNullOrWhiteSpace(meetup.VenueId)) throw new FormatException(nameof(meetup.VenueId));

            if (meetup.Sessions == null || meetup.Sessions.Count == 0 ||
                meetup.Sessions.Any(x => string.IsNullOrWhiteSpace(x.TalkId)))
                throw new FormatException(nameof(meetup.Sessions));

            return meetup;
        }

        public static MeetupVm ToVm(this Meetup meetup)
        {
            return new MeetupVm
            {
                Id = meetup.ExportId,
                Name = meetup.Name,
                CommunityId = (Communities) Enum.Parse(typeof(Communities), meetup.Community.ExportId, true),
                FriendIds = meetup.Friends.Select(x => new FriendReference {FriendId = x.Friend.ExportId}).ToList(),
                VenueId = meetup.Venue.ExportId,
                Sessions = meetup.Sessions.Select(x =>
                {
                    var timeZone = TZConvert.GetTimeZoneInfo(meetup.Community.TimeZone);
                    return new SessionVm
                    {
                        TalkId = x.Talk.ExportId,
                        StartTime = TimeZoneInfo.ConvertTimeFromUtc(x.StartTime, timeZone)
                            .ToString("yyyy-MM-ddTHH:mm:ss"),
                        EndTime = TimeZoneInfo.ConvertTimeFromUtc(x.EndTime, timeZone)
                            .ToString("yyyy-MM-ddTHH:mm:ss")
                    };
                }).ToList()
            };
        }
    }
}