using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Baskets.Core.Create;
using ShagOxServer.Application.Interfaces.Services.Base.Create;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Baskets.Core.Create;
public interface IBasketCreateService
    : ICreateOwnerService<
        CreateResponse,
        BasketCreateRequest
        >
{
    Task<Result<CreateResponse>> CreateInternalAsync(
        BasketCreateRequest request,
        long ownerId);
}
