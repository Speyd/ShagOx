namespace ShagOxServer.Domain.Filters.Baskets.BasketAttributes;
public sealed record BasketAttributeSearchFilter
(
    int? CategoryId,
    int? AttributeDefenitionId
);