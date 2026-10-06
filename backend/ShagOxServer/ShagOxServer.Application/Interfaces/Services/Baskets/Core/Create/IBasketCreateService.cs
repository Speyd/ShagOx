using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Baskets.Core.Create;
using ShagOxServer.Application.Interfaces.Services.Base;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Baskets.Core.Create;
public interface IBasketCreateService
    : ICreateService<
        CreateResponse,
        BasketCreateRequest
        >
{
    Task<Result<CreateResponse>> CreateInternalAsync(
        BasketCreateRequest request);
}