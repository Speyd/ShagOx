using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Services.Base.Localized;
using ShagOxServer.Domain.Entities.LineItems;

namespace ShagOxServer.Application.Services.Caches.Invalidations.LineItems.Localized;
public class LineItemAttributeLocalizedInvalidationService
    : BaseLocalizedCacheInvalidationService<LineItemAttribute>
{
    public LineItemAttributeLocalizedInvalidationService(
        IDataCacheService cache
    )
        : base(cache)
    {

    }
}
