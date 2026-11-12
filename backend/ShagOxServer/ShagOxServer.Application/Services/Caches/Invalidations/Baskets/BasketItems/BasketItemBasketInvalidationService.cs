using ShagOxServer.Application.DTOs.Baskets.Core.Cache;
using ShagOxServer.Application.DTOs.LineItems.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.Core;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Baskets.BasketItems;
public class BasketItemBasketInvalidationService
    : ICacheInvalidationService<BasketItem, LineItemCacheInfo>
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
       LineItemCacheInfo entityInfo)
    {
        var basketInfo = await GetBasketInfos(
            entityInfo.ParentId);

        if (basketInfo is null)
            return;

        await _basketInvalid.InvalidateUpdateAsync(basketInfo);
    }


    public async Task InvalidateDeleteAsync(
        LineItemCacheInfo entityInfo)
    {
        var basketInfo = await GetBasketInfos(
             entityInfo.ParentId);

        if (basketInfo is null)
            return;

        await _basketInvalid.InvalidateDeleteAsync(basketInfo);
    }

    public async Task InvalidateUpdateAsync(
        LineItemCacheInfo entityInfo)
    {
        var basketInfo = await GetBasketInfos(
             entityInfo.ParentId);

        if (basketInfo is null)
            return;

        await _basketInvalid.InvalidateUpdateAsync(basketInfo);
    }

    private async Task<BasketCacheInfo?> GetBasketInfos(
        long basketId)
    {
        var userId = await _basketRepository
           .GetUserIdByBasketAsync(basketId);

        if (!userId.HasValue)
            return null;

        return new BasketCacheInfo(
            basketId,
            userId.Value);
    }
}
