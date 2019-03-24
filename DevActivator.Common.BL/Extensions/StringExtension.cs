using System;
using DevActivator.Common.BL.Enums;

namespace DevActivator.Common.BL.Extensions
{
    public static class StringExtension
    {
        public static City GetCity(this string id)
            => (City) Enum.Parse(typeof(City), id.Substring(0, 3), true);
    }
}