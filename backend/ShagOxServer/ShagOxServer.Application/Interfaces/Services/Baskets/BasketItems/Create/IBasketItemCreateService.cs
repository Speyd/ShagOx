using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Baskets.BasketItems.Create;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Baskets.BasketItems.Create;
public interface IBasketItemCreateService
    : ICreateService<
        CreateResponse,
        BasketItemCreateRequest
        >
{
}