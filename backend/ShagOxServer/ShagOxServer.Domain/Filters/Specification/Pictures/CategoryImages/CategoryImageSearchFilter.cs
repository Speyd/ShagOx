namespace ShagOxServer.Domain.Filters.Specification.Pictures.CategoryImages;
public sealed record CategoryImageSearchFilter
(
    long? CategoryId,
    string? PublicId
) : PictureSearchFilter(PublicId);
