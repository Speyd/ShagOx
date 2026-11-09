using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;
using System.Linq.Expressions;
using System.Text.Json;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Core.Extensions;

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

        if (filter.CategoryId.HasValue)
        {
            query = query
                .Where(x => x.CategoryId == filter.CategoryId.Value)
                .OrderByDescending(x => x.Popularity);
        }

        if (filter.MinPrice.HasValue || filter.MaxPrice.HasValue)
        {
            query = query.Where(x =>
                (
                    x.Price.HasValue &&
                    (!filter.MinPrice.HasValue || x.Price.Value >= filter.MinPrice.Value) &&
                    (!filter.MaxPrice.HasValue || x.Price.Value <= filter.MaxPrice.Value)
                )
                ||
                x.Variants.Any(v =>
                    (!filter.MinPrice.HasValue || v.Price >= filter.MinPrice.Value) &&
                    (!filter.MaxPrice.HasValue || v.Price <= filter.MaxPrice.Value)
                )
            );
        }

        if (!string.IsNullOrWhiteSpace(filter.Attributes))
        {
            query = query.Where(x =>
                EF.Functions.JsonContains(
                    x.Attributes,
                    filter.Attributes)
                ||
                x.Variants.Any(v =>
                    EF.Functions.JsonContains(
                        v.Attributes,
                        filter.Attributes)));
        }

        return query;
    }
}
