using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;
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
            query = query.Where(x => x.CategoryId == filter.CategoryId.Value);
        }

        if (filter.CategoryIds is { Count: > 0 })
        {
            query = query.Where(x => filter.CategoryIds.Contains(x.CategoryId));
        }

        if (filter.InStock.HasValue)
        {
            query = filter.InStock.Value
                ? query.Where(x => x.Variants.Any(v => v.Stock > 0))
                : query.Where(x => !x.Variants.Any(v => v.Stock > 0));
        }

        if (filter.MinPrice.HasValue)
        {
            query = query.Where(x =>
                x.Variants.Any(v => v.Price >= filter.MinPrice.Value));
        }

        if (filter.MaxPrice.HasValue)
        {
            query = query.Where(x =>
                x.Variants.Any(v => v.Price <= filter.MaxPrice.Value));
        }

        if (filter.Attributes is not null)
        {
            JsonElement v;

            query = query.Where(x =>
                filter.Attributes.All(a =>
                    x.Attributes.RootElement.TryGetProperty(a, out v)));
        }

        return query;
    }
}