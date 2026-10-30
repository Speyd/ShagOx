using System.Text.Json;

namespace ShagOxServer.Domain.Filters.Advertisements;

public sealed record AdvertisementSearchFilter(
    string? Title,
    string? Description,
    long? CategoryId,
    string? Attributes,
    string? VariantAttributes = null,
    decimal? MinPrice = null,
    decimal? MaxPrice = null) : BaseFilter
{
    public static AdvertisementSearchFilter Create(
        AdvertisementSearchFilter filter)
    {
        return new AdvertisementSearchFilter(
            filter.Title,
            filter.Description,
            filter.CategoryId,
            NormalizeAttributes(filter.Attributes),
            NormalizeAttributes(filter.VariantAttributes),
            filter.MinPrice,
            filter.MaxPrice);
    }

    private static string? NormalizeAttributes(string? attributes)
    {
        if (string.IsNullOrWhiteSpace(attributes))
            return null;

        using var document = JsonDocument.Parse(attributes);
        var sorted = document.RootElement
            .EnumerateObject()
            .OrderBy(x => x.Name, StringComparer.Ordinal)
            .ToDictionary(
                x => x.Name,
                x => x.Value.Clone());

        return JsonSerializer.Serialize(sorted);
    }
}
