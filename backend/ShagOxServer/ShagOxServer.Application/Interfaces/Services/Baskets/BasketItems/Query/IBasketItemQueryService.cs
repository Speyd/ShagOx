using ShagOxServer.Application.DTOs.Baskets.BasketItems.Query;
using ShagOxServer.Application.DTOs.LineItems.Query;
using ShagOxServer.Application.Interfaces.Services.Base.Query;
using ShagOxServer.Application.Interfaces.Services.LineItems.Core.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.Domain.Filters.Baskets.BasketItems;

namespace ShagOxServer.Application.Interfaces.Services.Baskets.BasketItems.Query;
public interface IBasketItemQueryService
    : ILineItemQueryService<BasketItemDto,
        BasketItem, 
        BasketItemSearchFilter>
{
}
