using ShagOxServer.Application.Interfaces.Repositories.Baskets.Core;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Domain.Cache.Baskets;
using ShagOxServer.Domain.Cache.LineItems;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Baskets.BasketItems;
public class BasketItemBasketInvalidationService
    : ICacheInvalidationService<BasketItem, LineItemCacheContext>
{
    private readonly IBasketQueryRepository _basketRepository;
    private readonly BasketInvalidationService _basketInvalid;


    public BasketItemBasketInvalidationService(
        IBasketQueryRepository basketRepository,
        BasketInvalidationService basketInvalid)
    {
        _basketRepository = basketRepository;
        _basketInvalid = basketInvalid;
    }


    public async Task InvalidateCreateAsync(
       LineItemCacheContext entityInfo)
    {
        var basketInfo = await GetBasketInfos(
            entityInfo.ParentId);

        if (basketInfo is null)
            return;

        await _basketInvalid.InvalidateUpdateAsync(basketInfo);
    }


    public async Task InvalidateDeleteAsync(
        LineItemCacheContext entityInfo)
    {
        var basketInfo = await GetBasketInfos(
             entityInfo.ParentId);

        if (basketInfo is null)
            return;

        await _basketInvalid.InvalidateDeleteAsync(basketInfo);
    }

    public async Task InvalidateUpdateAsync(
        LineItemCacheContext entityInfo)
    {
        var basketInfo = await GetBasketInfos(
             entityInfo.ParentId);

        if (basketInfo is null)
            return;

        await _basketInvalid.InvalidateUpdateAsync(basketInfo);
    }

    private async Task<BasketCacheContext?> GetBasketInfos(
        long basketId)
    {
        var userId = await _basketRepository
           .GetUserIdByBasketAsync(basketId);

        if (!userId.HasValue)
            return null;

        return new BasketCacheContext(
            basketId,
            userId.Value);
    }
}
