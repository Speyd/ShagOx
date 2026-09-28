using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDictionaryValues.Extensions;
public static class AttributeDictionaryValueQueryExtensions
{
    public static IQueryable<AttributeDictionaryValue> WithIncludes(
        this IQueryable<AttributeDictionaryValue> query)
    {
        return query.Include(x => x.Dictionary);
    }
}