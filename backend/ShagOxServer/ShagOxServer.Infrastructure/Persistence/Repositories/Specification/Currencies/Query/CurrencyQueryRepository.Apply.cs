using ShagOxServer.Application.Interfaces.Repositories.Specification.Currencies;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Domain.Filters.Specification.Currencies;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Currencies.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Currencies.Query;
public partial class CurrencyQueryRepository
    : QueryRepository<Currency, CurrencySearchFilter>,
    ICurrencyQueryRepository
{
    protected override IQueryable<Currency> ApplyFilter(
        IQueryable<Currency> query,
        CurrencySearchFilter filter)
    {
        return query.Filter(filter);
    }
}