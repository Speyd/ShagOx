using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Baskets.Core.Create;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Baskets.Core.Create;
public interface IBasketCreateService
    : ICreateService<
        CreateResponse,
        BasketCreateRequest
        >
{
}