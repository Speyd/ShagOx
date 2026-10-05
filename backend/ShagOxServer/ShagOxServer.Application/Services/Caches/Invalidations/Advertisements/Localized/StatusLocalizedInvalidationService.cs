using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Services.Base.Localized;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Advertisements.Localized;
public class StatusLocalizedInvalidationService
    : BaseLocalizedCacheInvalidationService<Status>
{
    private protected AdvertisementLocalizedInvalidationService _advert;

    public StatusLocalizedInvalidationService(
        AdvertisementLocalizedInvalidationService advert,
        ICacheService cache
    )
        : base(cache)
    {
        _advert = advert;
    }


    public override async Task InvalidateLanguageAsync(
        string language)
    {
        await base.InvalidateLanguageAsync(language);

        await _advert.InvalidateLanguageAsync(language);
    }
}