using ShagOxServer.Application.Interfaces.Repositories.LineItems.Core;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketItems;
public interface IBasketItemExistsRepository
    : ILineItemExistsRepository<BasketItem>
{
}
