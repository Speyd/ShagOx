using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Services.Base.Localized;
using ShagOxServer.Domain.Entities.Location;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Location.Localized;
public class RegionLocalizedInvalidationService
    : BaseLocalizedCacheInvalidationService<Region>
{
    private protected CityLocalizedInvalidationService _city;


    public RegionLocalizedInvalidationService(
        CityLocalizedInvalidationService city,
        ICacheService cache
    )
        : base(cache)
    {
        _city = city;
    }


    public override async Task InvalidateLanguageAsync(
        string language)
    {
        await base.InvalidateLanguageAsync(language);

        await _city.InvalidateLanguageAsync(language);
    }
}