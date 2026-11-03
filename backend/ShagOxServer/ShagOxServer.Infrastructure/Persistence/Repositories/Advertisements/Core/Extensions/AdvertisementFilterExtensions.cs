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

        if (filter.MinPrice.HasValue)
        {
            query = query.Where(x => x.Variants.Any(v => v.Price >= filter.MinPrice.Value));
        }

        if (filter.MaxPrice.HasValue)
        {
            query = query.Where(x => x.Variants.Any(v => v.Price <= filter.MaxPrice.Value));
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

    //private static IQueryable<Advertisement> ApplyAttributeFilters(
    //    IQueryable<Advertisement> query,
    //    string filtersJson,
    //    bool isVariant)
    //{
    //    using var document = JsonDocument.Parse(filtersJson);

    //    foreach (var property in document.RootElement.EnumerateObject())
    //    {
    //        var values = property.Value.ValueKind == JsonValueKind.Array
    //            ? property.Value.EnumerateArray().ToArray()
    //            : [property.Value];

    //        query = query.Where(ad =>
    //            isVariant
    //                ? ad.Variants.Any(v =>
    //                    values.Any(value =>
    //                        EF.Functions.JsonContains(
    //                            v.Attributes,
    //                            JsonSerializer.Serialize(new Dictionary<string, JsonElement>
    //                            {
    //                                [property.Name] = value
    //                            }))))
    //                : EF.Functions.JsonContains(
    //                    ad.Attributes,
    //                    JsonSerializer.Serialize(new Dictionary<string, JsonElement>
    //                    {
    //                        [property.Name] = values[0]
    //                    })));
    //    }

    //    return query;
    //}
}
