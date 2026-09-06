using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Domain.Filters.Specification.Conditions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Conditions.Extensions;
public static class ConditionFilterExtensions
{
    public static IQueryable<Condition> Filter(
       this IQueryable<Condition> query,
       ConditionSearchFilter filter)
    {
        if (filter is null)
            return query;

        if (!string.IsNullOrWhiteSpace(filter.Code))
        {
            query = query.Where(u => u.Code != null &&
                EF.Functions.ILike(u.Code, $"%{filter.Code}%"));
        }

        return query;
    }
}
