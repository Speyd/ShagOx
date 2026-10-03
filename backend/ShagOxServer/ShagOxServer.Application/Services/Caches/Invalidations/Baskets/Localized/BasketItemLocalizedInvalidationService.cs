using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Services.Base.Localized;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Baskets.Localized;
public class BasketItemLocalizedInvalidationService
    : BaseLocalizedCacheInvalidationService<BasketItem>
{
    private readonly BasketLocalizedInvalidationService _basket;

    public BasketItemLocalizedInvalidationService(
        BasketLocalizedInvalidationService basket,
        ICacheService cache
    )
        : base(cache)
    {
        _basket = basket;
    }


    public override async Task InvalidateLanguageAsync(
        string language)
    {
        await base.InvalidateLanguageAsync(language);

        await _basket.InvalidateLanguageAsync(language);
    }
}