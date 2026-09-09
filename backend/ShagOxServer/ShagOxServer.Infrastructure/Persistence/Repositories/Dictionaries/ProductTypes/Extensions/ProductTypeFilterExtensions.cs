using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Filters.Dictionaries.ProductTypes;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.ProductTypes.Extensions;
public static class ProductTypeFilterExtensions
{
    public static IQueryable<ProductType> Filter(
      this IQueryable<ProductType> query,
      ProductTypeSearchFilter filter)
    {
        if (filter is null)
            return query;

        if (!string.IsNullOrWhiteSpace(filter.Code))
        {
            query = query.Where(u => u.Code != null &&
                EF.Functions.ILike(u.Code, $"%{filter.Code}%"));
        }

        if (!string.IsNullOrWhiteSpace(filter.Description))
        {
            query = query.Where(u => u.Description != null &&
                EF.Functions.ILike(u.Description, $"%{filter.Description}%"));
        }

        return query;
    }
}