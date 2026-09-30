using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes.Translations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Extensions;
public static class AttributeDictionaryValueTranslationQueryRepository
{
    public static IQueryable<AttributeDictionaryValueTranslation> WithIncludes(
        this IQueryable<AttributeDictionaryValueTranslation> query)
    {
        return query.Include(x => x.Translatable);
    }
}