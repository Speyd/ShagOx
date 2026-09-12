using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Domain.Filters.Baskets.Core;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Baskets.Core;
public interface IBasketQueryRepository
    : IQueryRepository<Basket>
{
    Task<Basket?> GetByUserAsync(
        int userId);

    Task<PagedResult<Basket>> Search(
        BasketSearchFilter filter,
        PaginationParams pagination);
}