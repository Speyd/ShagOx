using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Location.Translations;
using ShagOxServer.Domain.Filters.Location.Regions.Translations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Location.Regions.Translations.Extensions;
public static class RegionTranslationFilterExtensions
{
    public static IQueryable<RegionTranslation> Filter(
        this IQueryable<RegionTranslation> query,
        RegionTranslationSearchFilter filter)
    {
        if (filter is null)
            return query;

        if (!string.IsNullOrWhiteSpace(filter.RegionCode))
        {
            query = query.Where(x =>
                EF.Functions.ILike(x.Region.Code, $"%{filter.RegionCode}%"));
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