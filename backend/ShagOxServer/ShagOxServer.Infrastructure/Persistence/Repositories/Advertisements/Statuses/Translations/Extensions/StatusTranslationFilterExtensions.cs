using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Advertisements.Translations;
using ShagOxServer.Domain.Filters.Advertisements.Translations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Statuses.Translations.Extensions;
public static class StatusTranslationFilterExtensions
{
    public static IQueryable<StatusTranslation> Filter(
        this IQueryable<StatusTranslation> query,
        StatusTranslationSearchFilter filter)
    {
        if (filter is null)
            return query;

        if (!string.IsNullOrWhiteSpace(filter.StatusCode))
        {
            query = query.Where(x =>
                EF.Functions.ILike(x.Translatable.Code, $"%{filter.StatusCode}%"));
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

        if (!string.IsNullOrWhiteSpace(filter.Description))
        {
            query = query.Where(x =>
                EF.Functions.ILike(x.Description, $"%{filter.Description}%"));
        }

        return query;
    }
}