using Application.Common.Interfaces.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.MemoryCache.Common.Repositories;

public class MemoryCacheGenericListRepository<T>(
    IMemoryCache memoryCache
) : IGenericListRepository<T>
{
    public void Add(string key, T item, TimeSpan? expiresAtMinutes = null)
    {
        if (item is null)
        {
            return;
        }

        if (Exists(key))
        {
            return;
        }

        MemoryCacheEntryOptions options = new();
        options.SetSize(1);

        if (expiresAtMinutes != null)
        {
            options.SetAbsoluteExpiration(DateTime.Now.AddMinutes(expiresAtMinutes.Value.TotalMinutes));
        }

        memoryCache.Set(key, item, options);
    }

    public bool Exists(string key)
    {
        return memoryCache.TryGetValue(key, out _);
    }

    public void Remove(string key)
    {
        memoryCache.Remove(key);
    }
}
