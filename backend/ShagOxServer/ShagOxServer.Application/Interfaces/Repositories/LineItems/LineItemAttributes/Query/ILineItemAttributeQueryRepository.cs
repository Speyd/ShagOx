using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.LineItems;
using ShagOxServer.Domain.Filters.LineItems.LineItemAttributes;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketAttributes.Query;
public partial interface ILineItemAttributeQueryRepository
    : ISearchRepository<LineItemAttribute,
        LineItemAttributeSearchFilter>
{
    Task<PagedResult<LineItemAttribute>> GetByCategoryAsync(
        long categoryId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<LineItemAttribute?> GetByAttributeDefinitionAsync(
        long attributeDefinitionId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);
}
