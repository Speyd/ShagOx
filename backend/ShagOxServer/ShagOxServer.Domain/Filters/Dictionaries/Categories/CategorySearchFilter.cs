namespace ShagOxServer.Domain.Filters.Dictionaries.Categories;
public sealed record CategorySearchFilter
(
    string? Code = null,
    long? ProductTypeId = null
) : BaseFilter();