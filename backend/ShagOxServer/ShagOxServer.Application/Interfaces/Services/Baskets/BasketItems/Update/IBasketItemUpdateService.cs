using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Baskets.BasketItems.Update;
using ShagOxServer.Application.Interfaces.Services.Base.Update;

namespace ShagOxServer.Application.Interfaces.Services.Baskets.BasketItems.Update;
public interface IBasketItemUpdateService
    : IUpdateService<UpdateResponse, BasketItemUpdateRequest>,
    IEntityStatusUpdateService
{
}
