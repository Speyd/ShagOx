using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Specification.Translations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Conditions.Translations.Extensions;
public static class RegionTranslationQueryExtensions
{
    public static IQueryable<ConditionTranslation> WithIncludes(
       this IQueryable<ConditionTranslation> query)
    {
        return query
           .Include(x => x.Translatable);
    }
}