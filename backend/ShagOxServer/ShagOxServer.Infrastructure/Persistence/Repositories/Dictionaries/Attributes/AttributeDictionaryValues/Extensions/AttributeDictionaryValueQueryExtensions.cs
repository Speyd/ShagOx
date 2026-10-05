using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues.Cache;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDictionaryValues.Extensions;
public static class AttributeDictionaryValueQueryExtensions
{
    public static IQueryable<AttributeDictionaryValue> WithIncludes(
        this IQueryable<AttributeDictionaryValue> query)
    {
        return query.Include(x => x.Dictionary);
    }

    public static IQueryable<AttributeDictionaryValueCacheInfo> SelectCacheInfo(
        this IQueryable<AttributeDictionaryValue> query)
    {
        return query.Select(x => new AttributeDictionaryValueCacheInfo(
            x.Id,
            x.DictionaryId));
    }
}