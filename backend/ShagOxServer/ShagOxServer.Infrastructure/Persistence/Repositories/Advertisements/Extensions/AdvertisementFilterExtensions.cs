using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Advertisements;
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
            query = query.Where(u =>
                EF.Functions.ILike(u.Title, $"%{filter.Title}%"));
        }

        if (!string.IsNullOrWhiteSpace(filter.Description))
        {
            query = query.Where(u =>
                 EF.Functions.ILike(u.Description, $"%{filter.Description}%"));
        }

        if (filter.CategoryId is not null)
        {
            query = query
                .Where(x => 
                    x.CategoryId == filter.CategoryId.Value!)
                .OrderByDescending(x => x.Popularity);
        }
        return query;
    }
}
