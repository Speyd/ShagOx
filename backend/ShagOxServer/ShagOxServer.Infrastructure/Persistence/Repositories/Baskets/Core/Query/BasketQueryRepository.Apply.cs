using ShagOxServer.Application.Interfaces.Repositories.Baskets.Core;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Domain.Filters.Baskets.Core;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.Core.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.Core.Query;
public partial class BasketQueryRepository
    : SearchRepository<Basket, BasketSearchFilter>,
      IBasketQueryRepository
{
    protected override IQueryable<Basket> ApplyIncludes(
        IQueryable<Basket> query)
    {
        return query.WithIncludes();
    }

    protected override IQueryable<Basket> ApplyFilter(
        IQueryable<Basket> query,
        BasketSearchFilter filter)
    {
        return query.Filter(filter);
    }
}
