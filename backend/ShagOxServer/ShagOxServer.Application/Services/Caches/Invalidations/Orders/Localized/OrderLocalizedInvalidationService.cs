using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Services.Base.Localized;
using ShagOxServer.Domain.Entities.Orders;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Orders.Localized;
public class OrderLocalizedInvalidationService
    : BaseLocalizedCacheInvalidationService<Order>
{
    public OrderLocalizedInvalidationService(
        IDataCacheService cache
    )
        : base(cache)
    {

    }
}
