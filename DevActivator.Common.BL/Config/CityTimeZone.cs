using System;
using System.Collections.Generic;
using DevActivator.Common.BL.Enums;

namespace DevActivator.Common.BL.Config
{
    public static class CityTimeZone
    {
        public static TimeSpan GetTimeZone(this City city) => TimeZoneDic[city];

        private static readonly Dictionary<City, TimeSpan> TimeZoneDic = new Dictionary<City, TimeSpan>
        {
            {City.Spb, TimeSpan.FromHours(3)},
            {City.Msk, TimeSpan.FromHours(3)},
            {City.Sar, TimeSpan.FromHours(4)},
            {City.Kry, TimeSpan.FromHours(7)},
            {City.Kzn, TimeSpan.FromHours(3)},
            {City.Nsk, TimeSpan.FromHours(7)},
        };
    }
}