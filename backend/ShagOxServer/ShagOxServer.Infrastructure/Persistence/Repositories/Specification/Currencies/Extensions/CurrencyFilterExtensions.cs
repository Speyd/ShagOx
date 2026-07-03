using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Domain.Filters.Specification.Currencies;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Currencies.Extensions;
public static class CurrencyFilterExtensions
{
    public static IQueryable<Currency> Filter(
       this IQueryable<Currency> query,
       CurrencySearchFilter filter)
    {
        if (filter is null)
            return query;

        if (!string.IsNullOrWhiteSpace(filter.Code))
        {
            query = query.Where(u => u.Code.Contains(filter.Code));
        }

        if (!string.IsNullOrWhiteSpace(filter.Symbol))
        {
            query = query.Where(u => u.Symbol.Contains(filter.Symbol));
        }

        if (!string.IsNullOrWhiteSpace(filter.Name))
        {
            query = query.Where(u => u.Name.Contains(filter.Name));
        }

        return query;
    }
}
