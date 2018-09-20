using System;
using System.Collections.Generic;

namespace DotNetRu.Common.Collections
{
    public static class CollectionExtensions
    {
        public static void Assign<T>(this ICollection<T> dest, IEnumerable<T> source)
        {
            if (dest == null) throw new ArgumentNullException(nameof(dest));
            if (source == null) throw new ArgumentNullException(nameof(source));
            dest.Clear();
            foreach (var item in source)
                dest.Add(item);
        }
    }
}
