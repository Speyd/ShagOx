namespace ShagOxServer.Domain.Filters.Baskets.Core;
public sealed record BasketSearchFilter
(
    long? UserId
) : BaseFilter();