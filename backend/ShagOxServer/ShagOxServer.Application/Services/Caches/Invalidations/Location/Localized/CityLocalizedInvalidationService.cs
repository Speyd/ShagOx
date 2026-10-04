using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Services.Base.Localized;
using ShagOxServer.Application.Services.Caches.Invalidations.Auth.Localized;
using ShagOxServer.Domain.Entities.Location;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Location.Localized;
public class CityLocalizedInvalidationService
    : BaseLocalizedCacheInvalidationService<City>
{
    private protected UserLocalizedInvalidationService _user;


    public CityLocalizedInvalidationService(
        UserLocalizedInvalidationService user,
        ICacheService cache
    )
        : base(cache)
    {
        _user = user;
    }


    public override async Task InvalidateLanguageAsync(
        string language)
    {
        await base.InvalidateLanguageAsync(language);

        await _user.InvalidateLanguageAsync(language);
    }
}