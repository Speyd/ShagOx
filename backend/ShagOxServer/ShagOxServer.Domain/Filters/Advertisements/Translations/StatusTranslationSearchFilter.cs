using ShagOxServer.Domain.Entities.Advertisements.Enum;

namespace ShagOxServer.Domain.Filters.Advertisements.Translations;
public sealed record StatusTranslationSearchFilter
(
    AdvertisementStatus? StatusCode,
    string? Language,
    string? Name,
    string? Description
) : BaseFilter();
