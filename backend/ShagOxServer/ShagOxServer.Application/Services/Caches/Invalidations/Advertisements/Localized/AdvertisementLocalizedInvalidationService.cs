using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Services.Base.Localized;
using ShagOxServer.Application.Services.Caches.Invalidations.Baskets.Localized;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Advertisements.Localized;
public class AdvertisementLocalizedInvalidationService
    : BaseLocalizedCacheInvalidationService<Advertisement>
{
    private readonly BasketItemLocalizedInvalidationService _item;


    public AdvertisementLocalizedInvalidationService(
        BasketItemLocalizedInvalidationService item,
        ICacheService cache
    )
        : base(cache)
    {
        _item = item;
    }


    public override async Task InvalidateLanguageAsync(
        string language)
    {
        await base.InvalidateLanguageAsync(language);

        await _item.InvalidateLanguageAsync(language);
    }
}