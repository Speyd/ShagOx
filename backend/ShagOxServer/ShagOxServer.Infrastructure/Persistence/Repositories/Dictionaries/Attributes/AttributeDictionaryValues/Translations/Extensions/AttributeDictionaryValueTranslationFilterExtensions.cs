using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes.Translations;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitionValues.Translations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Extensions;
public static class AttributeDictionaryValueTranslationFilterExtensions
{
    public static IQueryable<AttributeDictionaryValueTranslation> Filter(
        this IQueryable<AttributeDictionaryValueTranslation> query,
        AttributeDictionaryValueTranslationSearchFilter filter)
    {
        if (filter is null)
            return query;

        if (!string.IsNullOrWhiteSpace(filter.DictionaryValueCode))
        {
            query = query.Where(x =>
                EF.Functions.ILike(x.Translatable.Code,
                $"%{filter.DictionaryValueCode}%"));
        }

        if (!string.IsNullOrWhiteSpace(filter.Language))
        {
            query = query.Where(x =>
                EF.Functions.ILike(x.Language, $"%{filter.Language}%"));
        }

        if (!string.IsNullOrWhiteSpace(filter.Name))
        {
            query = query.Where(x =>
                EF.Functions.ILike(x.Name, $"%{filter.Name}%"));
        }

        return query;
    }
}