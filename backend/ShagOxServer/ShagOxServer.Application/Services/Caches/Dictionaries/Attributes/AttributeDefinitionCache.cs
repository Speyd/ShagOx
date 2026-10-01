using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Domain.Caches;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;

namespace ShagOxServer.Application.Services.Caches.Dictionaries.Attributes;
public class AttributeDefinitionCache
{
    private static readonly string Prefix =
           typeof(AttributeDefinition).Name.ToLowerInvariant();


    public static async Task InvalidateDeleteAsync(
        ICacheService cache,
        AttributeDefinition cachedEntity)
    {
        await cache.RemoveAsync(
            CacheKeys.Entity<AttributeDefinition>(cachedEntity.Id));
    }

    public static async Task InvalidateUpdateAsync(
        ICacheService cache,
        AttributeDefinition cachedEntity)
    {
        await cache.RemoveAsync(
             CacheKeys.Entity<AttributeDefinition>(cachedEntity.Id));
    }
}