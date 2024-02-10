using Application.Interfaces.Repositories;
using Infrastructure.MemoryCache.Common.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.MemoryCache.Repositories;

public class MemoryCacheWhitelistRepository(
    IMemoryCache memoryCache
) : MemoryCacheGenericListRepository<bool>(memoryCache)
, IWhitelistRepository
{
    private readonly string _whitelistKeyPrefix = "whitelist-";

    public new void Add(string key, bool item, TimeSpan? expiresAtMinutes = null)
    {
        base.Add(_whitelistKeyPrefix + key, item, expiresAtMinutes);
    }

    public new bool Exists(string key)
    {
        return base.Exists(_whitelistKeyPrefix + key);
    }

    public new void Remove(string key)
    {
        base.Remove(_whitelistKeyPrefix + key);
    }
}
