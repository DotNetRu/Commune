using System;
using DotNetRuServer.Comon.BL.Enums;
using DotNetRuServer.Meetups.BL.Entities;

namespace DotNetRuServer.Integration.Common
{
    public static class VenueExtension
    {
        public static string GetCityTitle(this Venue venue)
        {
            switch (venue.City)
            {
                case City.Spb:
                    return "Санкт-Петербург";
                case City.Msk:
                    return "Москва";
                case City.Sar:
                    return "Саратов";
                case City.Kry:
                    return "Красноярск";
                case City.Kzn:
                    return "Казань";
                case City.Nsk:
                    return "Новосибирск";
                case City.Nnv:
                    return "Нижний Новгород";
                case City.Ufa:
                    return "Уфа";
                case City.Oms:
                    return "Омск";
                case City.Sam:
                    return "Самара";
                case City.Pnz:
                    return "Пенза";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}