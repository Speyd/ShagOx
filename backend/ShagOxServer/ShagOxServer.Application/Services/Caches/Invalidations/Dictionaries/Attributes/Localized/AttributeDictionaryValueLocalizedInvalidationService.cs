using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Services.Base.Localized;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Dictionaries.Attributes.Localized;
public class AttributeDictionaryValueLocalizedInvalidationService
    : BaseLocalizedCacheInvalidationService
        <AttributeDictionaryValue>
{
    public AttributeDictionaryValueLocalizedInvalidationService(
        ICacheService cache
    )
        : base(cache)
    {
    }
}