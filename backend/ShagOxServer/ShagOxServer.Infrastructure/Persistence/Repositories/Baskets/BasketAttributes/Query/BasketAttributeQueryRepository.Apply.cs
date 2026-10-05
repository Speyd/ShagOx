using ShagOxServer.Application.DTOs.Baskets.BasketAttributes.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketAttributes.Query;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Domain.Filters.Baskets.BasketAttributes;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketAttributes.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Baskets.BasketAttributes.Query;
public partial class BasketAttributeQueryRepository
    : QueryRepository<BasketAttribute, BasketAttributeSearchFilter>,
      IBasketAttributeQueryRepository
{
    protected override IQueryable<BasketAttribute> ApplyIncludes(
        IQueryable<BasketAttribute> query)
    {
        return query.WithIncludes();
    }

    protected override IQueryable<BasketAttribute> ApplyFilter(
      IQueryable<BasketAttribute> query,
      BasketAttributeSearchFilter filter)
    {
        return query.Filter(filter);
    }
}