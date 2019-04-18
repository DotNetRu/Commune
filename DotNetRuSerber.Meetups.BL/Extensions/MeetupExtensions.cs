using System;
using System.Linq;
using DotNetRuSerber.Meetups.BL.Entities;
using DotNetRuSerber.Meetups.BL.Models;

namespace DotNetRuSerber.Meetups.BL.Extensions
{
    public static class MeetupExtensions
    {
        public static MeetupVm EnsureIsValid(this MeetupVm meetup)
        {
            // todo: implement full validation
            if (string.IsNullOrWhiteSpace(meetup.Name))
            {
                throw new FormatException(nameof(meetup.Name));
            }

            if (meetup.FriendIds == null || meetup.FriendIds.Count == 0 ||
                meetup.FriendIds.Any(string.IsNullOrWhiteSpace))
            {
                throw new FormatException(nameof(meetup.FriendIds));
            }

            if (string.IsNullOrWhiteSpace(meetup.VenueId))
            {
                throw new FormatException(nameof(meetup.VenueId));
            }

            if (meetup.Sessions == null || meetup.Sessions.Count == 0 ||
                meetup.Sessions.Any(x => string.IsNullOrWhiteSpace(x.TalkId)))
            {
                throw new FormatException(nameof(meetup.Sessions));
            }

            return meetup;
        }

        public static MeetupVm ToVm(this Meetup meetup)
            =>
                new MeetupVm
                {
                    Id = meetup.ExportId,
                    Name = meetup.Name,
                    CommunityId = meetup.Community.ExportId,
                    FriendIds = meetup.Friends.Select(x => x.Friend.ExportId).ToList(),
                    VenueId = meetup.Venue.ExportId,
                    Sessions = meetup.Sessions.Select(x =>
                    {
                        var timeZone = TimeZoneInfo.FindSystemTimeZoneById(meetup.Community.TimeZone);
                        return new SessionVm
                        {
                            TalkId = x.Talk.ExportId,
                            StartTime = TimeZoneInfo.ConvertTimeFromUtc(x.StartTime, timeZone)
                                .ToString("yyyy-MM-ddTHH:mm:ss"),
                            EndTime = TimeZoneInfo.ConvertTimeFromUtc(x.EndTime, timeZone)
                                .ToString("yyyy-MM-ddTHH:mm:ss")
                        };
                    }).ToList(),
                };
    }
}