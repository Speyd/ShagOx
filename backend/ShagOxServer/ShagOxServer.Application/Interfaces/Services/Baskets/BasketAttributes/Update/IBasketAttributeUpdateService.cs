using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Baskets.Update;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Baskets.BasketAttributes.Update;
public interface IBasketAttributeUpdateService
    : IUpdateService<UpdateResponse, BasketAttributeUpdateRequest>
{
}