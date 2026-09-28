namespace ShagOxServer.Domain.Filters.Specification.Pictures;
public record PictureSearchFilter
(
    string? PublicId
) : BaseFilter();