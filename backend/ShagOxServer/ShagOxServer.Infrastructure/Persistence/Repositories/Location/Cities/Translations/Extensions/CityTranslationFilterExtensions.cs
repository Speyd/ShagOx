using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Location.Translations;
using ShagOxServer.Domain.Filters.Location.Cities.Translations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Location.Cities.Translations.Extensions;
public static class CityTranslationFilterExtensions
{
    public static IQueryable<CityTranslation> Filter(
        this IQueryable<CityTranslation> query,
        CityTranslationSearchFilter filter)
    {
        if (filter is null)
            return query;

        if (!string.IsNullOrWhiteSpace(filter.CityCode))
        {
            query = query.Where(x =>
                EF.Functions.ILike(x.Translatable.Code, $"%{filter.CityCode}%"));
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