using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;
using ShagOxServer.Domain.Filters.Dictionaries.AttributeDefinitions.Translations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.AttributeDefinitions.Translations.Extensions;
public static class AttributeDefinitionTranslationFilterExtensions
{
    public static IQueryable<AttributeDefinitionTranslation> Filter(
        this IQueryable<AttributeDefinitionTranslation> query,
        AttributeDefinitionTranslationSearchFilter filter)
    {
        if (filter is null)
            return query;

        if (!string.IsNullOrWhiteSpace(filter.AttributeDefinitionKey))
        {
            query = query.Where(x =>
                EF.Functions.ILike(x.Translatable.Key,
                $"%{filter.AttributeDefinitionKey}%"));
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