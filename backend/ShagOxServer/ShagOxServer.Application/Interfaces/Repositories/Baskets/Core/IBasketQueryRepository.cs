using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Domain.Filters.Baskets.Core;

namespace ShagOxServer.Application.Interfaces.Repositories.Baskets.Core;
public interface IBasketQueryRepository
    : ISearchRepository<Basket, BasketSearchFilter>
{
    Task<Basket?> GetByUserAsync(
        long userId,
        CancellationToken cancellationToken = default);

    Task<long?> GetIdByUserAsync(
        long userId,
        CancellationToken cancellationToken = default);

    Task<long?> GetUserIdByBasketAsync(
        long basketId,
        CancellationToken cancellationToken = default);
}
