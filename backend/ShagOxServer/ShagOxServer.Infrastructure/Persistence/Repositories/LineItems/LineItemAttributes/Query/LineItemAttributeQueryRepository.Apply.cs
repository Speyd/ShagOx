using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketAttributes.Query;
using ShagOxServer.Domain.Entities.LineItems;
using ShagOxServer.Domain.Filters.LineItems.LineItemAttributes;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.LineItems.LineItemAttributes.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.LineItems.LineItemAttributes.Query;
public partial class LineItemAttributeQueryRepository
    : SearchRepository<LineItemAttribute, LineItemAttributeSearchFilter>,
      ILineItemAttributeQueryRepository
{
    protected override IQueryable<LineItemAttribute> ApplyIncludes(
        IQueryable<LineItemAttribute> query)
    {
        return query.WithIncludes();
    }

    public override IQueryable<LineItemAttribute> ApplyFilter(
      IQueryable<LineItemAttribute> query,
      LineItemAttributeSearchFilter filter)
    {
        return query.Filter(filter);
    }
}
