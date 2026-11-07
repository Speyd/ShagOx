using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Baskets.BasketAttributes.Update;
using ShagOxServer.Application.Interfaces.Services.Base.Update;

namespace ShagOxServer.Application.Interfaces.Services.Baskets.BasketAttributes.Update;
public interface IBasketAttributeUpdateService
    : IUpdateService<UpdateResponse, BasketAttributeUpdateRequest>,
    IEntityStatusUpdateService
{
}
