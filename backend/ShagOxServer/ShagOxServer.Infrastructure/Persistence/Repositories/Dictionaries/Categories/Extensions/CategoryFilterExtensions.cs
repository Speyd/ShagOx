using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Filters.Dictionaries.Categories;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Categories.Extensions;
public static class CategoryFilterExtensions
{
    public static IQueryable<Category> Filter(
        this IQueryable<Category> query,
        CategorySearchFilter filter)
    {
        if (filter is null)
            return query;

        if (!string.IsNullOrWhiteSpace(filter.Name))
        {
            query = query.Where(u => u.Name != null &&
                EF.Functions.ILike(u.Name, $"%{filter.Name}%"));
        }

        if (filter.ProductTypeId is not null)
        {
            query = query
                .Where(u => u.ProductTypeId == filter.ProductTypeId);
        }
        
        return query;
    }
}
