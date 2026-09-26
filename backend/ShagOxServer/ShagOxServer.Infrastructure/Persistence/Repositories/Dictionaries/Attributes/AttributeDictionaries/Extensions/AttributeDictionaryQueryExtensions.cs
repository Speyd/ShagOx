using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDictionaries.Extensions;
public static class AttributeDictionaryQueryExtensions
{
    public static IQueryable<AttributeDictionary> WithIncludes(
        this IQueryable<AttributeDictionary> query)
    {
        return query.Include(x => x.Values)
            .Include(x => x.Attributes);

    }
}