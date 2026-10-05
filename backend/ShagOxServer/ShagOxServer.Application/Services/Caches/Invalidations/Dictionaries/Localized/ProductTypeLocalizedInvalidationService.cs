using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Services.Base.Localized;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Localized;
public class ProductTypeLocalizedInvalidationService
    : BaseLocalizedCacheInvalidationService<AttributeDefinition>
{
    private protected CategoryLocalizedInvalidationService _category;


    public ProductTypeLocalizedInvalidationService(
        CategoryLocalizedInvalidationService category,
        ICacheService cache
    )
        : base(cache)
    {
        _category = category;
    }


    public override async Task InvalidateLanguageAsync(
        string language)
    {
        await base.InvalidateLanguageAsync(language);

        await _category.InvalidateLanguageAsync(language);
    }
}