using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace DevActivator.Common.BL.Caching
{
    public class MemCache : ICache
    {
        private readonly IMemoryCache _cache;

        public MemCache(IMemoryCache cache)
        {
            _cache = cache;
        }

        public Task<T> GetOrCreateAsync<T>(string key, Func<ICacheEntry, Task<T>> factory)
            => _cache.GetOrCreateAsync(key, factory);

        public void Remove(string key)
            => _cache.Remove(key);

        public bool TryGetValue<T>(string key, out T value)
            => _cache.TryGetValue(key, out value);
    }
}