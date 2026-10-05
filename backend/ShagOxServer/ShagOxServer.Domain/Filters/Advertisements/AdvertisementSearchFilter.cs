namespace ShagOxServer.Domain.Filters.Advertisements;
public sealed record AdvertisementSearchFilter : BaseFilter
{
    public string? Title { get; init; }
    public string? Description { get; init; }
    public long? CategoryId { get; init; }
    public IReadOnlyList<string> Attributes { get; init; }


    public AdvertisementSearchFilter(
        string? title,
        string? description,
        long? categoryId,
        IEnumerable<string> attributes)
    {
        Title = title;
        Description = description;
        CategoryId = categoryId;

        Attributes = attributes
            .Order(StringComparer.OrdinalIgnoreCase)
            .ToList();
    }
}