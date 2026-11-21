using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Services.Base.Localized;
using ShagOxServer.Domain.Entities.Orders;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Orders.Localized;
public class OrderItemLocalizedInvalidationService
    : BaseLocalizedCacheInvalidationService<OrderItem>
{
    private readonly OrderLocalizedInvalidationService _order;

    public OrderItemLocalizedInvalidationService(
        OrderLocalizedInvalidationService order,
        IDataCacheService cache
    )
        : base(cache)
    {
        _order = order;
    }


    public override async Task InvalidateLanguageAsync(
        string language)
    {
        await base.InvalidateLanguageAsync(language);

        await _order.InvalidateLanguageAsync(language);
    }
}
