using Application.Interfaces.Repositories;
using Infrastructure.MemoryCache.Common.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.MemoryCache.Repositories;

public class MemoryCacheBlacklistRepository(
    IMemoryCache memoryCache
) : MemoryCacheGenericListRepository<bool>(memoryCache)
, IBlacklistRepository
{
    private readonly string _blacklistKeyPrefix = "blacklist-";

    public new void Add(string key, bool item, TimeSpan? expiresAtMinutes = null)
    {
        base.Add(_blacklistKeyPrefix + key, item, expiresAtMinutes);
    }

    public new bool Exists(string key)
    {
        return base.Exists(_blacklistKeyPrefix + key);
    }

    public new void Remove(string key)
    {
        base.Remove(_blacklistKeyPrefix + key);
    }
}
