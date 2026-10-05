using System.Text.Json;

namespace ShagOxServer.Domain.Filters.Advertisements;
public sealed record AdvertisementSearchFilter(
    string? Title,
    string? Description,
    long? CategoryId,
    JsonDocument? Attributes
) : BaseFilter
{
    public static AdvertisementSearchFilter Create(
        AdvertisementSearchFilter filter)
    {
        return new AdvertisementSearchFilter(
            filter.Title,
            filter.Description,
            filter.CategoryId,
            NormalizeAttributes(filter.Attributes));
    }

    private static JsonDocument? NormalizeAttributes(JsonDocument? attributes)
    {
        if (attributes is null)
            return null;

        var sorted = attributes.RootElement
            .EnumerateObject()
            .OrderBy(x => x.Name, StringComparer.Ordinal)
            .ToDictionary(
                x => x.Name,
                x => x.Value.Clone());

        return JsonSerializer.SerializeToDocument(sorted);
    }
}