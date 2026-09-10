using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;
using ShagOxServer.Domain.Filters.Dictionaries.Categories.Translations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Categories.Translations.Extensions;
public static class CategoryTranslationFilterExtensions
{
    public static IQueryable<CategoryTranslation> Filter(
        this IQueryable<CategoryTranslation> query,
        CategoryTranslationSearchFilter filter)
    {
        if (filter is null)
            return query;

        if (!string.IsNullOrWhiteSpace(filter.CategoryCode))
        {
            query = query.Where(x =>
                EF.Functions.ILike(x.Translatable.Code,
                $"%{filter.CategoryCode}%"));
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