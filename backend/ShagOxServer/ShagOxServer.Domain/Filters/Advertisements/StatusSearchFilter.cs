using ShagOxServer.Domain.Entities.Advertisements.Enum;

namespace ShagOxServer.Domain.Filters.Advertisements;
public sealed record StatusSearchFilter
(
    AdvertisementStatus? Code
) : BaseFilter();
