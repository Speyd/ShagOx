using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Services.Base.Localized;
using ShagOxServer.Application.Services.Caches.Invalidations.Advertisements.Localized;
using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Auth.Localized;
public class UserLocalizedInvalidationService
    : BaseLocalizedCacheInvalidationService<User>
{
    private protected AdvertisementLocalizedInvalidationService _advert;


    public UserLocalizedInvalidationService(
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