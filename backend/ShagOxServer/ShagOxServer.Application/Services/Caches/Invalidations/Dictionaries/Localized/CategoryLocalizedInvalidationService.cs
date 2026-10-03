using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Services.Base.Localized;
using ShagOxServer.Application.Services.Caches.Invalidations.Advertisements.Localized;
using ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Attributes.Localized;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Localized;
public class CategoryLocalizedInvalidationService
    : BaseLocalizedCacheInvalidationService<AttributeDefinition>
{
    private protected AttributeDefinitionLocalizedInvalidationService _attribute;
    private protected AdvertisementLocalizedInvalidationService _advert;


    public CategoryLocalizedInvalidationService(
        AttributeDefinitionLocalizedInvalidationService attribute,
        AdvertisementLocalizedInvalidationService advert,
        ICacheService cache
    )
        : base(cache)
    {
        _attribute = attribute;
        _advert = advert;
    }


    public override async Task InvalidateLanguageAsync(
        string language)
    {
        await base.InvalidateLanguageAsync(language);

        await _attribute.InvalidateLanguageAsync(language);

        await _advert.InvalidateLanguageAsync(language);
    }
}