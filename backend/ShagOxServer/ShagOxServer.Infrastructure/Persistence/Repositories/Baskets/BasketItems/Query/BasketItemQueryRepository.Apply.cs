using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketItems.Query;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Domain.Filters.Baskets.BasketItems;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketItems.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.LineItems.Core.Query;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketItems.Query;
public partial class BasketItemQueryRepository
    : LineItemQueryRepository<BasketItem, BasketItemSearchFilter>,
      IBasketItemQueryRepository
{
    protected override IQueryable<BasketItem> ApplyIncludes(
        IQueryable<BasketItem> query)
    {
        return query.WithIncludes();
    }

    public override IQueryable<BasketItem> ApplyFilter(
      IQueryable<BasketItem> query,
      BasketItemSearchFilter filter)
    {
        return query.Filter(filter);
    }
}
