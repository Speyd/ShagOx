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
            query = query.Where(u => u.Name.Contains(filter.Name));
        }

        return query;
    }
}
