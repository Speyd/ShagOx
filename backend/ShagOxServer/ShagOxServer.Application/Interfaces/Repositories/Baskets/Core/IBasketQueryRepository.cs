using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Domain.Filters.Baskets.Core;

namespace ShagOxServer.Application.Interfaces.Repositories.Baskets.Core;
public interface IBasketQueryRepository
    : IQueryRepository<Basket, BasketSearchFilter>
{
    Task<Basket?> GetByUserAsync(
        long userId);
}