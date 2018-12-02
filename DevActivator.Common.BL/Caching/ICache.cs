using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace DevActivator.Common.BL.Caching
{
    public interface ICache
    {
        Task<T> GetOrCreateAsync<T>(string key, Func<ICacheEntry, Task<T>> factory);

        void Remove(string key);

        bool TryGetValue<T>(string key, out T value);
    }
}