using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Cache.Dictionaries.Attributes;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDictionaryValues.Extensions;
public static class AttributeDictionaryValueQueryExtensions
{
    public static IQueryable<AttributeDictionaryValue> WithIncludes(
        this IQueryable<AttributeDictionaryValue> query)
    {
        return query
            .AsSplitQuery()
            .Include(x => x.Dictionary);
    }

    public static IQueryable<AttributeDictionaryValueCacheContext> SelectCacheInfo(
        this IQueryable<AttributeDictionaryValue> query,
        EntityStatus status = EntityStatus.Active)
    {
        return query.Select(x => new AttributeDictionaryValueCacheContext(
            x.Id,
            x.DictionaryId,
            status));
    }
}
