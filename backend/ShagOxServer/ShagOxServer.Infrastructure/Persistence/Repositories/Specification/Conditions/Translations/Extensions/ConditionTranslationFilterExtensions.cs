using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Specification.Translations;
using ShagOxServer.Domain.Filters.Specification.Conditions.Translations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Conditions.Translations.Extensions;
public static class ConditionTranslationFilterExtensions
{
    public static IQueryable<ConditionTranslation> Filter(
        this IQueryable<ConditionTranslation> query,
        ConditionTranslationSearchFilter filter)
    {
        if (filter is null)
            return query;

        if (!string.IsNullOrWhiteSpace(filter.ConditionCode))
        {
            query = query.Where(x =>
                EF.Functions.ILike(x.Translatable.Code, $"%{filter.ConditionCode}%"));
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