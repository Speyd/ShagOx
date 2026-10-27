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

        if (filter.Attributes is not null)
        {
            query = ApplyAttributeFilters(
                query,
                filter.Attributes,
                isVariant: false);
        }

        if (filter.VariantAttributes is not null)
        {
            query = ApplyAttributeFilters(
                query,
                filter.VariantAttributes,
                isVariant: true);
        }

        return query;
    }

    private static IQueryable<Advertisement> ApplyAttributeFilters(
        IQueryable<Advertisement> query,
        string filtersJson,
        bool isVariant)
    {
        using var filters = JsonDocument.Parse(filtersJson);
        var filterProperties = filters.RootElement
            .EnumerateObject()
            .Select(property => new
            {
                property.Name,
                Values = property.Value.ValueKind == JsonValueKind.Array
                    ? property.Value.EnumerateArray()
                        .Select(value => value.Clone())
                        .ToArray()
                    : [property.Value.Clone()]
            })
            .Where(property => property.Values.Length > 0)
            .ToArray();

        if (filterProperties.Length == 0)
            return query;

        if (isVariant)
        {
            var advertisementParameter = Expression.Parameter(
                typeof(Advertisement), "advertisement");
            var variantParameter = Expression.Parameter(
                typeof(AdvertisementVariant), "variant");
            Expression variantBody = Expression.Constant(true);

            foreach (var property in filterProperties)
            {
                var candidateMatches = BuildCandidateMatches(
                    Expression.Property(
                        variantParameter,
                        nameof(AdvertisementVariant.Attributes)),
                    property.Name,
                    property.Values);
                variantBody = Expression.AndAlso(variantBody, candidateMatches);
            }

            var variantPredicate = Expression.Lambda<
                Func<AdvertisementVariant, bool>>(
                    variantBody,
                    variantParameter);
            var anyVariant = Expression.Call(
                typeof(Enumerable),
                nameof(Enumerable.Any),
                [typeof(AdvertisementVariant)],
                Expression.Property(
                    advertisementParameter,
                    nameof(Advertisement.Variants)),
                variantPredicate);

            return query.Where(Expression.Lambda<Func<Advertisement, bool>>(
                anyVariant,
                advertisementParameter));
        }

        foreach (var property in filterProperties)
        {
            var advertisementParameter = Expression.Parameter(
                typeof(Advertisement), "advertisement");
            var matches = BuildCandidateMatches(
                Expression.Property(
                    advertisementParameter,
                    nameof(Advertisement.Attributes)),
                property.Name,
                property.Values);

            query = query.Where(Expression.Lambda<Func<Advertisement, bool>>(
                matches,
                advertisementParameter));
        }

        return query;
    }

    private static Expression BuildCandidateMatches(
        Expression attributesExpression,
        string key,
        JsonElement[] selectedValues)
    {
        Expression? matches = null;

        foreach (var selectedValue in selectedValues)
        {
            var candidate = JsonSerializer.SerializeToDocument(
                new Dictionary<string, JsonElement>
                {
                    [key] = selectedValue
                });
            var contains = Expression.Call(
                typeof(NpgsqlJsonDbFunctionsExtensions),
                nameof(NpgsqlJsonDbFunctionsExtensions.JsonContains),
                Type.EmptyTypes,
                Expression.Property(null, typeof(EF).GetProperty(
                    nameof(EF.Functions))!),
                attributesExpression,
                Expression.Constant(candidate, typeof(object)));

            matches = matches is null
                ? contains
                : Expression.OrElse(matches, contains);
        }

        return matches ?? Expression.Constant(false);
    }
}
