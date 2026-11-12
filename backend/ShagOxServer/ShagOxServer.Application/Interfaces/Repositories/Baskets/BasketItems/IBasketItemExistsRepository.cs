using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Base.Special;
using ShagOxServer.Application.Interfaces.Repositories.LineItems.Core;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketItems;
public interface IBasketItemExistsRepository
    : ILineItemExistsRepository<BasketItem>
{
}
