using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Baskets.Create;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Baskets.BasketAttributes.Create;
public interface IBasketAttributeCreateService
    : ICreateService<
        CreateResponse,
        BasketAttributeCreateRequest
        >
{
}