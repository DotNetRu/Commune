using System;
using DevActivator.Common.BL.Extensions;
using DevActivator.Meetups.BL.Entities;
using DevActivator.Meetups.BL.Models;

namespace DevActivator.Meetups.BL.Extensions
{
    public static class VenueExtensions
    {
        public static VenueVm EnsureIsValid(this VenueVm venue)
        {
            // todo: implement full validation
            if (string.IsNullOrWhiteSpace(venue.Name))
            {
                throw new FormatException(nameof(venue.Name));
            }

            if (string.IsNullOrWhiteSpace(venue.Address))
            {
                throw new FormatException(nameof(venue.Address));
            }

            if (venue.City != venue.Id.GetCity())
            {
                throw new FormatException(nameof(venue.City));
            }

            return venue;
        }


        public static VenueVm ToVm(this Venue venue)
            => new VenueVm
            {
                Id = venue.Id,
                City = venue.Id.GetCity(),
                Name = venue.Name,
                Address = venue.Address,
                MapUrl = venue.MapUrl
            };

        public static Venue Extend(this Venue original, VenueVm venue)
            => new Venue
            {
                Id = original.Id,
                Name = venue.Name,
                Address = venue.Address,
                MapUrl = venue.MapUrl
            };
    }
}