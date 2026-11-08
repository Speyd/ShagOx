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

        query = query.FilterByAttributes(filter.Attributes);
        query = query.FilterByAttributes(filter.VariantAttributes);

        return query;
    }

    private static IQueryable<Advertisement> FilterByAttributes(
        this IQueryable<Advertisement> query,
        string? attributesJson)
    {
        if (string.IsNullOrWhiteSpace(attributesJson))
            return query;

        using var document = JsonDocument.Parse(attributesJson);

        foreach (var property in document.RootElement.EnumerateObject())
        {
            var values = property.Value.ValueKind == JsonValueKind.Array
                ? property.Value.EnumerateArray().ToList()
                : [property.Value];

            Expression<Func<Advertisement, bool>>? condition = null;

            foreach (var json in values.SelectMany(v => ContainmentCandidates(property.Name, v)))
            {
                var next = HasAttribute(json);
                condition = condition is null ? next : Or(condition, next);
            }

            if (condition is not null)
                query = query.Where(condition);
        }

        return query;
    }

    private static IEnumerable<string> ContainmentCandidates(string key, JsonElement value)
    {
        yield return JsonSerializer.Serialize(new Dictionary<string, JsonElement> { [key] = value });
        yield return JsonSerializer.Serialize(new Dictionary<string, JsonElement[]> { [key] = [value] });
    }

    private static Expression<Func<Advertisement, bool>> HasAttribute(string json) =>
        x => EF.Functions.JsonContains(x.Attributes, json)
            || x.Variants.Any(v => EF.Functions.JsonContains(v.Attributes, json));

    private static Expression<Func<Advertisement, bool>> Or(
        Expression<Func<Advertisement, bool>> left,
        Expression<Func<Advertisement, bool>> right)
    {
        var parameter = Expression.Parameter(typeof(Advertisement), "x");
        var body = Expression.OrElse(
            Expression.Invoke(left, parameter),
            Expression.Invoke(right, parameter));

        return Expression.Lambda<Func<Advertisement, bool>>(body, parameter);
    }
}
