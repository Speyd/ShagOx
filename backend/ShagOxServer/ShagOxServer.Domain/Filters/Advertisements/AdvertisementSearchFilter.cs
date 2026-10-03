namespace ShagOxServer.Domain.Filters.Advertisements;
public sealed record AdvertisementSearchFilter
(
    string? Title = null,
    string? Description = null,
    long? CategoryId = null,
    List<string>? Attributes = null,
    List<long>? CategoryIds = null,
    bool? InStock = null,
    decimal? MinPrice = null,
    decimal? MaxPrice = null,
    List<string>? Memory = null,
    List<string>? Colors = null,
    string? SortBy = null,
    string? SortOrder = null
) : BaseFilter();