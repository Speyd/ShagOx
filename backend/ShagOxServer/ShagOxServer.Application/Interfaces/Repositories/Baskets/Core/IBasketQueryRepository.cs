using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Domain.Filters.Baskets.Core;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Baskets.Core;
public interface IBasketQueryRepository
    : IQueryRepository<Basket>
{
    Task<PagedResult<Basket>> GetByUserAsync(
        int userId,
        PaginationParams pagination);

    Task<PagedResult<Basket>> Search(
        BasketSearchFilter filter,
        PaginationParams pagination);
}