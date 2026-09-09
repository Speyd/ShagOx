using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;
using ShagOxServer.Domain.Filters.Dictionaries.ProductTypes.Translations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.ProductTypes.Translations.Extensions;
public static class ProductTypeTranslationFilterExtensions
{
    public static IQueryable<ProductTypeTranslation> Filter(
        this IQueryable<ProductTypeTranslation> query,
        ProductTypeTranslationSearchFilter filter)
    {
        if (filter is null)
            return query;

        if (!string.IsNullOrWhiteSpace(filter.ProductTypeCode))
        {
            query = query.Where(x =>
                EF.Functions.ILike(x.Translatable.Code,
                $"%{filter.ProductTypeCode}%"));
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