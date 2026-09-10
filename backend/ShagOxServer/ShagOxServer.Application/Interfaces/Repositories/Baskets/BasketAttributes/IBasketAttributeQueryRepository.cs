using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Domain.Filters.Baskets.BasketAttributes;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketAttributes;
public interface IBasketAttributeQueryRepository
    : IQueryRepository<BasketAttribute>
{
    Task<PagedResult<BasketAttribute>> GetByCategoryAsync(
        int categoryId,
        PaginationParams pagination);

    Task<BasketAttribute?> GetByAttributeDefenitionAsync(
        int attributeDefenitionId);

    Task<PagedResult<BasketAttribute>> Search(
        BasketAttributeSearchFilter filter,
        PaginationParams pagination);
}