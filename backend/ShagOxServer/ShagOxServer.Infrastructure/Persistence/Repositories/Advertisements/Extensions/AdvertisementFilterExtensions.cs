using ShagOxServer.Domain.Entities;
using ShagOxServer.Domain.Filters.Advertisements;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Extensions;
public static class AdvertisementFilterExtensions
{
    public static IQueryable<Advertisement> Filter(
        this IQueryable<Advertisement> query,
        AdvertisementSearchFilter filter)
    {
        if (filter is null)
            return query;

        if (!string.IsNullOrWhiteSpace(filter.Title))
        {
            query = query.Where(u => u.Title.Contains(filter.Title));
        }

        if (!string.IsNullOrWhiteSpace(filter.Description))
        {
            query = query.Where(u => u.Description.Contains(filter.Description));
        }

        return query;
    }
}
