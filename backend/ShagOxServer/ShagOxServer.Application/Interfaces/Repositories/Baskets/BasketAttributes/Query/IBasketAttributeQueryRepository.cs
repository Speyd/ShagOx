using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Domain.Filters.Baskets.BasketAttributes;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketAttributes.Query;
public partial interface IBasketAttributeQueryRepository
    : IQueryRepository<BasketAttribute, 
        BasketAttributeSearchFilter>
{
    Task<PagedResult<BasketAttribute>> GetByCategoryAsync(
        long categoryId,
        PaginationParams pagination);

    Task<BasketAttribute?> GetByAttributeDefinitionAsync(
        long attributeDefinitionId);
}