using System;
using DotNetRuServer.Comon.BL.Extensions;
using DotNetRuServer.Meetups.BL.Entities;
using DotNetRuServer.Meetups.BL.Models;

namespace DotNetRuServer.Meetups.BL.Extensions
{
    public static class VenueExtensions
    {
        public static VenueVm EnsureIsValid(this VenueVm venue)
        {
            // todo: implement full validation
            if (string.IsNullOrWhiteSpace(venue.Name)) throw new FormatException(nameof(venue.Name));

            if (string.IsNullOrWhiteSpace(venue.Address)) throw new FormatException(nameof(venue.Address));

            if (venue.City != venue.Id.GetCity()) throw new FormatException(nameof(venue.City));

            return venue;
        }


        public static VenueVm ToVm(this Venue venue)
        {
            return new VenueVm
            {
                Id = venue.ExportId,
                City = venue.City,
                Name = venue.Name,
                Address = venue.Address,
                MapUrl = venue.MapUrl
            };
        }

        public static Venue Extend(this Venue original, VenueVm venue)
        {
            return new Venue
            {
                Id = original.Id,
                ExportId = venue.Id,
                Name = venue.Name,
                City = venue.City,
                Address = venue.Address,
                MapUrl = venue.MapUrl
            };
        }
    }
}