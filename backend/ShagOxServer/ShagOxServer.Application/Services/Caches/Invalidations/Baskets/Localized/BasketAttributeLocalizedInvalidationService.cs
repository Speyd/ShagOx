using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Services.Base.Localized;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Baskets.Localized;
public class BasketAttributeLocalizedInvalidationService
    : BaseLocalizedCacheInvalidationService<BasketAttribute>
{
    public BasketAttributeLocalizedInvalidationService(
        ICacheService cache
    )
        : base(cache)
    {

    }
}