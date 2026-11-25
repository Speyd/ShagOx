using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketItems.Query;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Invalidations.Baskets.BasketItems;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Application.Services.Caches.Keys.Baskets;
using ShagOxServer.Domain.Cache.Baskets;
using ShagOxServer.Domain.Cache.LineItems;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Baskets;
public class BasketInvalidationService
    : ICacheInvalidationService<Basket, BasketCacheContext>
{
    private readonly IBasketItemQueryRepository _itemRepository;
    private readonly BasketItemInvalidationService _itemInvalid;

    private readonly IDataCacheService _cache;


    public BasketInvalidationService(
        IBasketItemQueryRepository itemRepository,
        BasketItemInvalidationService itemInvalid,
        IDataCacheService cache)
    {
        _itemRepository = itemRepository;
        _itemInvalid = itemInvalid;
        _cache = cache;
    }


    public async Task InvalidateCreateAsync(
        BasketCacheContext entityInfo)
    {
    }

    public async Task InvalidateDeleteAsync(
       BasketCacheContext entityInfo)
    {
        await InvalidateAsync(entityInfo);

        var itemInfos = await GetBasketItemInfos(
            entityInfo.Id);

        foreach (var itemInfo in itemInfos) 
        {
            await _itemInvalid
                .InvalidateDeleteAsync(itemInfo);
        }
    }

    public async Task InvalidateUpdateAsync(
        BasketCacheContext entityInfo)
    {
        await InvalidateAsync(entityInfo);
    }

    private async Task InvalidateAsync(
        BasketCacheContext entityInfo)
    {
        await _cache.RemoveByPatternAsync(CacheKeys
           .EntityLanguagePattern<Basket>(entityInfo.Id));

        await _cache.RemoveByPatternAsync(BasketCache
          .ByUserPattern(entityInfo.UserId));
    }

    private async Task<List<LineItemCacheContext>> GetBasketItemInfos(
        long basketId)
    {
        return await _itemRepository
            .GetCacheInfosByParentAsync(basketId);
    }
}
