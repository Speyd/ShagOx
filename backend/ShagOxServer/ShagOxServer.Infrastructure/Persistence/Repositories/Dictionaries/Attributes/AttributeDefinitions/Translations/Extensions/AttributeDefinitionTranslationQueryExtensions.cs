using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes.Translations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDefinitions.Translations.Extensions;
public static class AttributeDefinitionTranslationQueryExtensions
{
    public static IQueryable<AttributeDefinitionTranslation> WithIncludes(
        this IQueryable<AttributeDefinitionTranslation> query)
    {
        return query.Include(x => x.Translatable);
    }
}