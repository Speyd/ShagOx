using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Services.Base.Localized;
using ShagOxServer.Application.Services.Caches.Invalidations.Baskets.Localized;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Attributes.Localized;
public class AttributeDefinitionLocalizedInvalidationService
    : BaseLocalizedCacheInvalidationService<AttributeDefinition>
{
    private protected BasketAttributeLocalizedInvalidationService _attribute;

    public AttributeDefinitionLocalizedInvalidationService(
        BasketAttributeLocalizedInvalidationService attribute,
        ICacheService cache
    )
        : base(cache)
    {
        _attribute = attribute;
    }


    public override async Task InvalidateLanguageAsync(
        string language)
    {
        await base.InvalidateLanguageAsync(language);

        await _attribute.InvalidateLanguageAsync(language);
    }
}