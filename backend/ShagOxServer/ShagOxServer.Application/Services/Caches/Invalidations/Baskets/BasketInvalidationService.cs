using ShagOxServer.Application.DTOs.Baskets.BasketItems.Cache;
using ShagOxServer.Application.DTOs.Baskets.Core.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketItems;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Invalidations.Baskets.BasketItems;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Application.Services.Caches.Keys.Baskets;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Baskets;
public class BasketInvalidationService
    : ICacheInvalidationService<Basket, BasketCacheInfo>
{
    private readonly IBasketItemQueryRepository _itemRepository;
    private readonly BasketItemInvalidationService _itemInvalid;

    private readonly ICacheService _cache;


    public BasketInvalidationService(
        IBasketItemQueryRepository itemRepository,
        BasketItemInvalidationService itemInvalid,
        ICacheService cache)
    {
        _itemRepository = itemRepository;
        _itemInvalid = itemInvalid;
        _cache = cache;
    }


    public async Task InvalidateDeleteAsync(
       BasketCacheInfo entityInfo)
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
        BasketCacheInfo entityInfo)
    {
        await InvalidateAsync(entityInfo);
    }

    private async Task InvalidateAsync(
        BasketCacheInfo entityInfo)
    {
        await _cache.RemoveByPatternAsync(CacheKeys
           .EntityLanguagePattern<Basket>(entityInfo.Id));

        await _cache.RemoveByPatternAsync(BasketCache
          .ByUserPattern(entityInfo.UserId));
    }

    private async Task<List<BasketItemCacheInfo>> GetBasketItemInfos(
        long basketId)
    {
        return await _itemRepository
            .GetCacheInfosByBasketAsync(basketId);
    }
}