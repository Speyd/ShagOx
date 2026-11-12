using ShagOxServer.Application.Interfaces.Repositories.Base.Items.Query;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Domain.Filters.Baskets.BasketItems;

namespace ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketItems.Query;
public partial interface IBasketItemQueryRepository
    : ILineItemQueryRepository<BasketItem, BasketItemSearchFilter>
{
}
