using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketAttributes;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Domain.Caches;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Services.Caches.Baskets;
public class BasketAttributeCache
{
    private static readonly string Prefix =
           typeof(BasketAttributeCache).Name.ToLowerInvariant();


    public static string ByAttributeDefenition(
           long attributeId)
           => $"{Prefix}:attribute-defenition:{attributeId}";

    public static string ByCategoryId(
           long categoryId)
           => $"{Prefix}:category:{categoryId}";

    public static async Task InvalidateDeleteAsync(
        ICacheService cache,
        BasketAttribute cachedEntity)
    {
        await cache.RemoveAsync(
            CacheKeys.Entity<BasketAttribute>(cachedEntity.Id));

        await cache.RemoveAsync(
            ByAttributeDefenition(
                cachedEntity.AttributeDefinitionId));

        await cache.RemoveAsync(
           ByAttributeDefenition(
               cachedEntity.AttributeDefinition.CategoryId));
    }

    public static async Task InvalidateUpdateAsync(
        ICacheService cache,
        BasketAttribute cachedEntity)
    {
        await cache.RemoveAsync(
             CacheKeys.Entity<BasketAttribute>(cachedEntity.Id));

        await cache.RemoveAsync(
            ByAttributeDefenition(
                cachedEntity.AttributeDefinitionId));

        await cache.RemoveAsync(
           ByAttributeDefenition(
               cachedEntity.AttributeDefinition.CategoryId));
    }

    public static async Task InvalidateByAttributeDefinitionAsync(
        ICacheService cache,
        IBasketAttributeQueryRepository repository,
        long attributeId)
    {
        var attribute =
            await repository.GetByAttributeDefenitionAsync(
                attributeId);

        if (attribute is null)
            return;

        await cache.RemoveAsync(
             CacheKeys.Entity<BasketAttribute>(attribute.Id));

        await cache.RemoveAsync(
            ByAttributeDefenition(
                attribute.AttributeDefinitionId));

        await cache.RemoveAsync(
           ByAttributeDefenition(
               attribute.AttributeDefinition.CategoryId));
    }
}